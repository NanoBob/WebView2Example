import React, { useEffect, useState } from 'react';
import logo from './logo.svg';
import './App.css';

interface WeatherForecast {
  date: string,
  temperatureC: string,
  temperatureF: string,
  summary: string
}

async function fetchWeather() {
  const response = await fetch("/WeatherForecast");
  return await response.json();
}

function App() {
  const [weather, setWeather] = useState<WeatherForecast>();
  useEffect(() => {
    fetchWeather().then(setWeather)
  }, [])

  return (
    <div className="App">
      <header className="App-header">
        <img src={logo} className="App-logo" alt="logo" />
        <p>
          Edit <code>src/App.tsx</code> and save to reload.
        </p>
        <a
          className="App-link"
          href="https://reactjs.org"
          target="_blank"
          rel="noopener noreferrer"
        >
          Learn React
        </a>
        <span>{ JSON.stringify(weather) }</span>
      </header>
    </div>
  );
}

export default App;
