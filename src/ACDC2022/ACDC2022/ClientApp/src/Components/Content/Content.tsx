import { useEffect, useState } from 'react';
import { Stack } from '@fluentui/react';
import * as signalR from "@microsoft/signalr";
import * as atlas from 'azure-maps-control';

import '@fluentui/react/dist/css/fabric.css';
import './Content.css';
import 'antd/dist/antd.css';
import { Telemetry } from '../../Models/Telemetry';

const Content = () => {

    const [positions, setPositions] = useState([] as Telemetry[]);
    const [map, setMap] = useState({} as atlas.Map);

    const onConnected = (connection: any) => {
        if (window.location.href.indexOf('/none') <= 0) {
            setInterval(() => {
                if (window.location.href.indexOf('/check') > 0) {
                    if (navigator.geolocation) {
                        navigator.geolocation.getCurrentPosition((position) => {
                            const check = `checkID,${position.coords.longitude},${position.coords.latitude}`;
                            connection.send('requestData', check);
                        });
                    }
                } else {
                    connection.send('requestData', '');
                }
            }, 2000);
        }
    }

    /* SIGNAL R */
    useEffect(() => {
        const connection = new signalR.HubConnectionBuilder().withUrl(`https://app-acdc2022.azurewebsites.net/telemetry`).build();

        connection!.on("responseData", (data: Telemetry[]) => {
            setPositions(data);
        });

        connection!.start()
            .then(function () {
                onConnected(connection);
            })
            .catch(function (error) {
                console.error(error.message);
            });
    }, []);

    /* MAP */
    useEffect(() => {
        const map = new atlas.Map('myMap', {
            center: [10.664696709431993, 59.975094689960905],
            zoom: 18.3,
            pitch: 50,
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

        setMap(map);

        // Wait until the map resources are ready.
        map.events.add('ready', function () {
            //Construct a pitch control and add it to the map.
            map.controls.add(new atlas.control.PitchControl(), {
                position: atlas.ControlPosition.TopRight
            });

            // Construct a zoom control
            var zoomControl = new atlas.control.ZoomControl();

            // Add the zoom control to the map
            map.controls.add(zoomControl, {
                position: atlas.ControlPosition.BottomRight
            });
        });
    }, []);

    useEffect(() => {

        if (Object.keys(map).length > 0 && positions.length > 0) {
            map.markers.clear();

            //Create a HTML marker and add it to the map.
            positions.forEach((p, i) => {
                const position = [p.data.geolocation.lon, p.data.geolocation.lat];
                map.markers.add(new atlas.HtmlMarker({
                    htmlContent: `<image src="https://s3-us-west-2.amazonaws.com/s.cdpn.io/1717245/ylw-pushpin.png" style="pointer-events: none;" />`,
                    position,
                    pixelOffset: [6, -15]
                }));
            });

            /*map.markers.getMarkers().forEach((m, i) => {
                const position = positions.find((p) => m.getElement().innerHTML.indexOf(`${p.deviceId}`) > 0);
                if (position && position.data) {
                    const options = m.getOptions();
                    options.position = [position.data.geolocation.lon, position.data.geolocation.lat];
                    m.setOptions(options);
                }
            });*/
        }

    }, [positions]);

    // Test code testing SignalR Hub
    return (
        <div>
            <div className="wrapper">
                <Stack>
                    <div id="myMap"></div>
                </Stack>
            </div>
        </div>
    );
};

export default Content;