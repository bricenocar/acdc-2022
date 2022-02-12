import { useEffect, useState } from 'react';
import { ethers } from 'ethers';
import * as api from '../Services/Api';
import { User } from '../Models/User';
import { UserAuth } from "../Models/Auth/UserAuth";

interface Ethereumish {
    autoRefreshOnNetworkChange: boolean;
    chainId: string;
    isMetaMask?: boolean;
    isStatus?: boolean;
    networkVersion: string;
    selectedAddress: any;

    request?: (request: { method: string, params?: Array<any> }) => Promise<any>
}

declare global {
    interface Window {
        ethereum: Ethereumish;
    }
}

export const AuthProvider = () => {

    const initialuser = {} as User;
    const [user, setUser] = useState(initialuser);
    const [isUserAuthenticated, setIsUserAuthenticated] = useState(false);

    const signIn = async () => {
        if (window.ethereum && window.ethereum.request) {
            try {
                // Configure provider
                const provider = new ethers.providers.Web3Provider(window.ethereum);
                await provider.send('eth_requestAccounts', []);

                // Get walletId and signature
                const signer = provider.getSigner();
                const walletId = await signer.getAddress();
                const signature = await signer.signMessage('Sign in to the application provider.');

                // Build user auth object
                const userAuth = { walletId, signature } as UserAuth
                const response = await api.signIn(userAuth);

                if (response.ok && response.data) {
                    const user = { ...response?.data, authenticated: true } as User;
                    setUser(user);
                    setIsUserAuthenticated(true);

                    return user;
                }
            } catch (error) {
                alert(error);
            }
        }
        else {
            alert('Metamask was not found');
        }

        return null;
    };

    const signOut = async () => {
        try {
            const response = await api.signOut();
            if (response.ok) {
                setUser(initialuser);
                setIsUserAuthenticated(false);

                return true;
            }
        } catch (error) {
            alert(error);
        }

        return false;
    };

    const signCheck = async () => {
        try {
            const response = await api.signCheck();
            if (response.ok) {
                const user = response?.data;

                if (user) {
                    setUser(user);
                    setIsUserAuthenticated(true);

                    return true;
                }
            }
            else {
                alert('Error checking if user is authenticated');
            }
        }
        catch (error) {
            alert(error);
        }

        return false;
    }

    useEffect(() => {
        signCheck();
    }, []);

    return {
        user,
        isUserAuthenticated,
        signIn,
        signOut,
    };
}