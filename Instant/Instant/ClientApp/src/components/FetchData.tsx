import React, {useEffect, useState} from 'react';

export interface Forecast {
    date: number
    temperatureC: number
    temperatureF: number
    summary: string
}

export function FetchData() {
    let [forecasts, setForecasts] = useState<Forecast[]>([])
    let [loading, setLoading] = useState(true)

    async function populateWeatherData() {
        const response = await fetch('weatherforecast')
        const data: Forecast[] = await response.json()
        return data
    }

    useEffect(() => {
        populateWeatherData().then(data => {
            setForecasts(data)
            setLoading(false)
        })
    }, [])

    function renderForecastsTable(forecasts: Forecast[]) {
        return (
            <table className='table table-striped' aria-labelledby="tabelLabel">
                <thead>
                <tr>
                    <th>Date</th>
                    <th>Temp. (C)</th>
                    <th>Temp. (F)</th>
                    <th>Summary</th>
                </tr>
                </thead>
                <tbody>
                {forecasts.map(forecast =>
                    <tr key={forecast.date}>
                        <td>{forecast.date}</td>
                        <td>{forecast.temperatureC}</td>
                        <td>{forecast.temperatureF}</td>
                        <td>{forecast.summary}</td>
                    </tr>
                )}
                </tbody>
            </table>
        );
    }


    let contents = loading
        ? <p><em>Loading...</em></p>
        : renderForecastsTable(forecasts);

    return (
        <div>
            <h1 id="tabelLabel">Weather forecast</h1>
            <p>This component demonstrates fetching data from the server.</p>
            {contents}
        </div>
    );
}