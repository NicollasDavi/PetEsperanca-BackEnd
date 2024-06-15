import React, { useEffect, useState } from 'react';
import OngCard from './OngCard';
import axiosInstance from '../services/axios/axiosInstance';
import { useSearch } from '../contexts/SearchContext';
import './OngList.css'; 

const OngList = () => {
  const { searchTerm } = useSearch();
  const [ongsList, setOngsList] = useState<{ ongName: string, id: string }[]>([]);

  const fetchOngs = () => {
    axiosInstance.get('/list/ong').then((response) => {
      setOngsList(response.data);
    }).catch((error) => {
      console.log("Erro ao buscar ONGs:", error);
    });
  };

  useEffect(() => {
    fetchOngs();
  }, []);

  const handleDelete = async (id: string) => {
    try {
      await axiosInstance.delete(`/ong/delete/${id}`);
      fetchOngs();
    } catch (error) {
      console.log("Erro ao deletar ONG:", error);
    }
  };

  const filteredOngs = ongsList.filter(ong =>
    ong.ongName.toLowerCase().includes(searchTerm.toLowerCase())
  );

  return (
    <div>
      {filteredOngs.length === 0 ? (
        <p className="no-results-message">Nenhum resultado encontrado para "{searchTerm}".</p>
      ) : (
        filteredOngs.map((ong, index) => (
          <OngCard
            nome={ong.ongName}
            key={index}
            onDelete={() => handleDelete(ong.id)}
            imagemUrl=''
            id={ong.id}
          />
        ))
      )}
    </div>
  );
};

export default OngList;
