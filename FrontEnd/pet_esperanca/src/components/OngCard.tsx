import React from 'react';
import { OngCardProps } from '../types/OngCardProps';
import { MdDeleteForever } from 'react-icons/md';
import './ongcard.css';

const OngCard = ({ nome, imagemUrl, onDelete }: OngCardProps) => {
  return (
    <div className='ong-card'>
      <img src={imagemUrl} alt={`${nome}`} className='ong-image' />
      <section>
        <h1>{nome}</h1>
        <MdDeleteForever className='delete' onClick={onDelete} />
      </section>
    </div>
  );
};

export default OngCard;
