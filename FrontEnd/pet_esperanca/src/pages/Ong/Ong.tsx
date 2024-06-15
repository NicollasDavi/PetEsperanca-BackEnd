import React from 'react';
import { useParams } from 'react-router-dom';
import "./ongpage.css"

const Ong = () => {
  const { id } = useParams();

  return (
    <div>
        <section className='ong-apresentation'>
            <div className='ong-image-container'>
                <img src="" alt="" />
            </div>
            <div className='ong-daescription'>
                <h1>OngName</h1>
                <p>Lorem ipsum dolor, sit amet consectetur adipisicing elit. Architecto fuga, magnam minus pariatur cumque ipsam velit rerum necessitatibus culpa, saepe perspiciatis! Dolores suscipit enim sed est quas autem minima repellat.</p>
            </div>
        </section>
        <section className='coment-contaier'>
            <textarea placeholder='Escreva um comentario' />
        </section>
    </div>
  );
}

export default Ong;
