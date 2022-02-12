export interface Telemetry {
    id: string;
    deviceId: string;
    deviceType: string;
    data: Data;
    timestamp: string;
}

export interface Data {
    accelerometerX: number;
    accelerometerY: number;
    accelerometerZ: number;

    gyroscopeX: number;
    gyroscopeY: number;
    gyroscopeZ: number;

    humidity: number;
    magnetometerX: number;
    magnetometerY: number;
    magnetometerZ: number;

    pressure: number;
    temperature: number;
    geolocation: Geolocation;
    battery: number;
    barometer: number;
}

export interface Geolocation {
    lat: number;
    lon: number;
    alt: number;
}