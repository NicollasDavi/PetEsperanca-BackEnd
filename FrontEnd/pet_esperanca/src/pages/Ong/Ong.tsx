import React from 'react';
import { useParams } from 'react-router-dom';

const Ong = () => {
  const { id } = useParams();

  return (
    <div>
        <section className='ong-apresentation'>
            <div className='ong-image-container'>
                <img src="" alt="" />
            </div>
            <div>
                <h1>OngName</h1>
                
            </div>
        </section>
    </div>
  );
}

export default Ong;
