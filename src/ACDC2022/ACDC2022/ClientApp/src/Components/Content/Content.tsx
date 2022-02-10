import { useState } from 'react';
import { Label, PrimaryButton, Stack, TextField } from '@fluentui/react';
import * as signalR from "@microsoft/signalr";

import '@fluentui/react/dist/css/fabric.css';
import './Content.css';
import 'antd/dist/antd.css';

const Content = () => {

    const divMessages = document.querySelector("#divMessages");
    const tbMessage: HTMLInputElement | null = document.querySelector("#tbMessage");
    let username = new Date().getTime();

    const connection = new signalR.HubConnectionBuilder()
        // .withUrl("https://localhost:44417/location")
        .withUrl("https://localhost:44417/location", {
            skipNegotiation: true,
            transport: signalR.HttpTransportType.WebSockets
        }).build();

    connection.on("messageReceived", (username: string, message: string) => {
        let messages = document.createElement("div");

        messages.innerHTML =
            `<div class="message-author">${username}</div><div>${message}</div>`;

        divMessages!.appendChild(messages);
        divMessages!.scrollTop = divMessages!.scrollHeight;
    });

    connection.start().catch(err => document.write(err));

    const onKeyUp = (e: any) => {
        if (e.key === "Enter") {
            send();
        }
    };

    const onClick = () => {
        debugger;
        send();
    };

    const send = () => {
        connection.send("broadcastMessage", username, "test")
            .then(() => username = new Date().getTime());
    }

    // Test code testing SignalR Hub
    return (
        <div>
            <div className="wrapper">

                <Stack id="divMessages" className="messages"></Stack>
                <Stack>
                    <Label id="lblMessage">Message:</Label>
                    <TextField id="tbMessage" onKeyUp={(e) => onKeyUp(e)}></TextField>
                    <PrimaryButton id="btnSend" onClick={onClick}>Send</PrimaryButton>
                </Stack>
            </div>
        </div>
    );
};

export default Content;