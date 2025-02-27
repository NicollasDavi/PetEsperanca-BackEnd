import React from 'react';
import Header from './components/Header';
import { BrowserRouter as Router, Route, Routes } from 'react-router-dom';

import Home from './pages/Home';
import Ongs from './pages/Ong/Ongs';
import CreateOng from './pages/Ong/CreateOng';
import SignIn from './pages/SigIn/SigIn';
import Ong from './pages/Ong/Ong';


function App() {
  return (
    <Router>
      <header className="App-header">
        <Header />
      </header>
      <Routes>
        <Route path='/home' element={<Home />}/>
        <Route path='/ongs' element={<Ongs />}/>
        <Route path='/newong' element={<CreateOng />}/>
        <Route path='/signin' element={<SignIn />}/>
        <Route path='/ong/:id/:action' element={<Ong />}/>
      </Routes>
    </Router>
  );
}

export default App;
