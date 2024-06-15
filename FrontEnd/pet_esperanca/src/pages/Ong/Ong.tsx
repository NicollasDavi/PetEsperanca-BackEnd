import React, { useEffect, useState } from 'react';
import { useParams } from 'react-router-dom';
import "./ongpage.css";
import Button from '../../components/UI/Button';
import Comment from '../../components/Comment';
import axiosInstance from "../../services/axios/axiosInstance"

interface Comment {
    id: string,
    comment: string
}

interface Ong {
  id: string;
  ongName: string;
  cnpj: string
}

const Ong = () => {
  const { id } = useParams();

  const [comment, setComments] = useState<Comment[]>([]);
  const [ong, setOng] = useState<Ong>()

  useEffect(() => {
    axiosInstance.get(`/ong/${id}`).then((response) => {
      console.log(response.data)
      setOng(response.data)
    })

    axiosInstance.get(`/comments/${id}`).then((response) => {
      console.log(response.data)
      setComments(response.data)
    })
  }, [])
  const handleAddComment = () => {
  
  };

  return (
    <div>
        <section className='ong-apresentation'>
            <div className='ong-image-container'>
                <img src="https://via.placeholder.com/150" alt="Ong" />
            </div>
            <div className='ong-description'>
                <h1>{ong?.ongName}</h1>
                <p>Lorem ipsum dolor, sit amet consectetur adipisicing elit. Architecto fuga, magnam minus pariatur cumque ipsam velit rerum necessitatibus culpa, saepe perspiciatis! Dolores suscipit enim sed est quas autem minima repellat.</p>
            </div>
        </section>
        <section className='comment-container'>
            <textarea
              placeholder='Escreva um comentário'
              rows={5}
            />
            <Button text='Enviar Comentario' type='env' url='' func={() => setTimeout}/>
        </section>
        <section className='comments-list'>
            {comment.map((comment, index) => (
                <Comment initialText={comment.comment} onDelete={() => setTimeout} key={index}/>
            ))}
        </section>
    </div>
  );
}

export default Ong;
