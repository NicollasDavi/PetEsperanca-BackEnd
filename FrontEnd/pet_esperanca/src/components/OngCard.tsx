import React from 'react';
import { useNavigate } from 'react-router-dom';
import { OngCardProps } from '../types/OngCardProps';
import { MdDeleteForever } from 'react-icons/md';
import './ongcard.css';

const OngCard = ({ nome, imagemUrl, id, onDelete }: OngCardProps) => {
  const navigate = useNavigate();

  const handleRedirect = () => {
    navigate(`/ong/${id}`);
  };

  return (
    <div className='ong-card' onClick={handleRedirect}>
      <img src={imagemUrl} alt={`${nome}`} className='ong-image' />
      <section>
        <h1>{nome}</h1>
        <MdDeleteForever className='delete' onClick={(e) => {
          e.stopPropagation();
          onDelete();
        }} />
      </section>
    </div>
  );
};

export default OngCard;
