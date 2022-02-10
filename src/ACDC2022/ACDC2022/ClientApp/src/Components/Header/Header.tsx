import React, { useContext, useEffect, useState } from 'react';
import { IconButton } from '@fluentui/react/lib/Button';
import { useBoolean } from '@fluentui/react-hooks';
import { IButtonStyles, Text, FontWeights, Stack, Coachmark, DirectionalHint } from '@fluentui/react';
import { mergeStyles, AnimationClassNames } from '@fluentui/react/lib/Styling';
import { AuthContext } from '../../App';
import { CommonDialog } from '../Common/CommonDialog';

const Header = () => {

    // Get auth provider
    const useAuth: any = useContext(AuthContext);

    // State
    const [hideDialog, setHideDialog] = useState(true);
    const [isCoachmarkVisible, { setFalse: hideCoachmark, setTrue: showCoachmark }] = useBoolean(false);
    const [coachmarkPosition, setCoachmarkPosition] = React.useState<DirectionalHint>(DirectionalHint.bottomAutoEdge);

    const targetButton = React.useRef<HTMLDivElement>(null);

    // Icon styles
    const iconStyles: IButtonStyles = {
        root: {
            color: 'blue',
            fontSize: 30,
            fontWeight: FontWeights.regular,
        },
    };
    const layerStyles = mergeStyles([
        {
            color: "black",
            lineHeight: '50px',
            opacity: '0'
        },
        AnimationClassNames.scaleUpIn100,
    ]);

    // Click Events
    const signInClick = async () => {
        const user = await useAuth.signIn();
        if (user?.WalletId) {
            hideCoachmark();
        } else {
            alert('Could not get user!');
        }
    }
    const signOutClick = async () => {
        const response = await useAuth.signOut();
        if (!response) {
            alert('Could not sign out the user!');
        }
    }
    const showDialogClick = () => {
        setHideDialog(false);
    }

    // Dialog Events
    const cancelDialogEvent = async () => {
        setHideDialog(true);
    }
    const okDialogEvent = async () => {
        setHideDialog(true);
        signOutClick();
    }

    const positioningContainerProps = React.useMemo(
        () => ({
            directionalHint: coachmarkPosition,
            doNotLayer: false,
        }),
        [coachmarkPosition],
    );

    useEffect(() => {
        // Show/Hide coach mark
        if (!useAuth.isUserAuthenticated) {
            showCoachmark();
        }
        else {
            hideCoachmark();
        }
    }, [useAuth.isUserAuthenticated]);

    return (
        <Stack>
            <div className={layerStyles} style={{ backgroundColor: "whitesmoke" }}>
                <div style={{ margin: "0 20px" }}>
                    <Text>
                        Wallet: {useAuth?.user?.WalletId}
                    </Text>
                    <div style={{ float: "right" }} ref={targetButton}>
                        {
                            !useAuth.isUserAuthenticated &&
                            <IconButton iconProps={{ iconName: 'Signin' }} onClick={signInClick} styles={iconStyles} title="Sign in" ariaLabel="Sign in" aria-hidden={false} />
                        }
                        {
                            useAuth.isUserAuthenticated &&
                            <IconButton iconProps={{ iconName: 'SignOut' }} onClick={showDialogClick} styles={iconStyles} title="Sign out" ariaLabel="Sign out" aria-hidden={false} />
                        }
                    </div>
                </div>
            </div>

            <CommonDialog title='Login' textOk='Si' textCancel='No' subText='Desea desconectar la wallet?' maxWidth={450} hideDialog={hideDialog} cancelEvent={cancelDialogEvent} okEvent={okDialogEvent}></CommonDialog>

            {isCoachmarkVisible && (
                <Coachmark
                    target={targetButton.current}
                    positioningContainerProps={positioningContainerProps}
                    ariaAlertText="A coachmark has appeared"
                    ariaDescribedBy="coachmark-desc1"
                    ariaLabelledBy="coachmark-label1"
                    ariaDescribedByText="Press enter or alt + C to open the coachmark notification"
                    ariaLabelledByText="Coachmark notification"
                />
            )}
        </Stack>
    );
}

export default Header;