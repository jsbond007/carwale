import React from 'react';
import logo from './logo.svg';
import './App.css';
import AppRouting from './app-routing';
import { ToastContainer } from "react-toastify";
import 'react-toastify/dist/ReactToastify.css';

function App() {
  return (
    <div className="App">
      <ToastContainer/>
      <AppRouting/>
    </div>
  );
}

export default App;
