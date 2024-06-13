import React from 'react';
import { FaHandsHelping } from "react-icons/fa";
import { FaSearch } from 'react-icons/fa';
import './Header.css';

const Header = () => {
  return (
    <div className='header-container'>
      <div className='logo'>
        <FaHandsHelping size={30} />
        <h1>Pet Esperança</h1>
      </div>
      <section className='linkContainer'>
        <ul>
          <li>
            <a href="/home">Home</a>
          </li>
          <li>
            <a href="/ongs">Ongs</a>
          </li>
        </ul>
      </section>
      <div className='search-bar'>
        <input type="text" placeholder="Search..." />
        <button>
          <FaSearch />
        </button>
      </div>
      <section className='authContainer'>
        <ul>
          <li>
            <a href="/signin">Sign In</a>
          </li>
        </ul>
      </section>
    </div>
  );
};

export default Header;
