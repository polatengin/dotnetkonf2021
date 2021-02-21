import React from 'react';
import logo from './logo.svg';
import './App.css';

function App() {
  return (
    <div className="App">
      <header className="App-header">
        <img src={logo} className="App-logo" alt="logo" />
        <p>
          Edit <code>src/App.tsx</code> and save to reload.
        </p>
        <button onClick={() => {

          fetch("/api/products/").then(r => r.json().then(json => {
            console.log(json);
          }));

        }}>Get Product List</button>
        <button onClick={() => {

          fetch("/api/products/random").then(r => r.json().then(json => {
            console.log(json);
          }));

        }}>Get Random Product</button>
        <a
          className="App-link"
          href="https://reactjs.org"
          target="_blank"
          rel="noopener noreferrer"
        >
          Learn React
        </a>
      </header>
    </div>
  );
}

export default App;
