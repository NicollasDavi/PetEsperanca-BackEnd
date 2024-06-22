import React from 'react';
import { useNavigate } from 'react-router-dom';
import { OngCardProps } from '../types/OngCardProps';
import { MdDeleteForever } from 'react-icons/md';
import { RiEdit2Fill } from "react-icons/ri";
import './ongcard.css';

const OngCard = ({ nome, imagemUrl, id, onDelete }: OngCardProps) => {
  const navigate = useNavigate();

  const handleRedirect = (action : number) => {
    action === 0 ? navigate(`/ong/${id}/0`) : navigate(`/ong/${id}/1`);
  };

  return (
    <div className='ong-card'>
      <section onClick={() => handleRedirect(0)}>
        <img src={imagemUrl} alt={nome} className='ong-image' />
      </section>
      <section>
        <section onClick={() => handleRedirect(0)}>
        <h1>{nome}</h1>
      </section>
        <section className="buttons">
        <RiEdit2Fill className='edit' onClick={() => handleRedirect(1)}/>
        <MdDeleteForever className='delete' onClick={(e) => {
          e.stopPropagation();
          onDelete();
        }} />
        </section>
        
      </section>
    </div>
  );
};

export default OngCard;
