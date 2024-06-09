import React from 'react';
import Header from './components/Header';
import { BrowserRouter as Router, Route, Routes } from 'react-router-dom';

import Home from './pages/Home';
import Ongs from './pages/Ongs';

function App() {
  return (
    <Router>
      <header className="App-header">
        <Header />
      </header>
      <Routes>
        <Route path='/home' element={<Home />}/>
        <Route path='/ongs' element={<Ongs />}/>
      </Routes>
    </Router>
  );
}

export default App;
