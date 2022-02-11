import { useEffect, useState } from 'react';
import { Label, Separator, Stack } from '@fluentui/react';
import * as signalR from "@microsoft/signalr";
import * as atlas from 'azure-maps-control';

import '@fluentui/react/dist/css/fabric.css';
import './Content.css';
import 'antd/dist/antd.css';

const Content = () => {

    const [position, setPosition] = useState('');

    const onConnected = (connection: any) => {
        setInterval(() => {
            connection.send('requestData');
        }, 2000);
    }

    useEffect(() => {
        const connection = new signalR.HubConnectionBuilder().withUrl('https://localhost:7052/telemetry').build();

        connection!.on("responseData", (data: string) => {
            setPosition(data);
        });

        connection!.start()
            .then(function () {
                onConnected(connection);
            })
            .catch(function (error) {
                console.error(error.message);
            });
    }, []);

    useEffect(() => {
        const map = new atlas.Map('myMap', {
            center: [10.664696709431993, 59.975094689960905],
            zoom: 18.5,
            view: 'Auto',
            language: 'en-US',
            showBuildingModels: true,
            renderWorldCopies: true,
            showLogo: false,
            showFeedbackLink: false,
            style: 'satellite_road_labels',
            authOptions: {
                authType: atlas.AuthenticationType.subscriptionKey,
                subscriptionKey: 'YAbaX7I6g_-21K39a6IWCCszOYOAkqybS4LWO4tGawI'
            }
        });

        //Wait until the map resources are ready.
        map.events.add('ready', function () {

            //Construct a pitch control and add it to the map.
            map.controls.add(new atlas.control.PitchControl(), {
                position: atlas.ControlPosition.TopRight
            });

            /* Construct a zoom control*/
            var zoomControl = new atlas.control.ZoomControl();

            /* Add the zoom control to the map*/
            map.controls.add(zoomControl, {
                position: atlas.ControlPosition.BottomRight
            });

            //Create a HTML marker and add it to the map.
            map.markers.add(new atlas.HtmlMarker({
                htmlContent: "<div><div class='pin bounce' style='background-color:#77b3e4'></div><div class='pulse'></div></div>",
                position: [10.66468, 59.97509],
                pixelOffset: [5, -18]
            }));

        });
    }, []);

    // Test code testing SignalR Hub
    return (
        <div>
            <div className="wrapper">
                <Stack>
                    <Label>{position}</Label>
                    <Separator />
                    <div id="myMap"></div>
                </Stack>
            </div>
        </div>
    );
};

export default Content;