import React, { useState } from 'react';
import { useParams } from 'react-router-dom';
import "./ongpage.css";
import Button from '../../components/UI/Button';
import Comment from '../../components/Comment';

interface Comment {
    id: string,
    text: string
}

const Ong = () => {
  const { id } = useParams();

  const [comments, setComments] = useState<Comment[]>([
    { id: "1", text: "This is a great organization!" },
    { id: "2", text: "Really inspiring work." }
  ]);
  const [newComment, setNewComment] = useState('');

  const handleAddComment = () => {
    if (newComment.trim()) {
      setComments([...comments, { id: comments.length + 1 + "", text: newComment }]);
      setNewComment('');
    }
  };

  return (
    <div>
        <section className='ong-apresentation'>
            <div className='ong-image-container'>
                <img src="https://via.placeholder.com/150" alt="Ong" />
            </div>
            <div className='ong-description'>
                <h1>OngName</h1>
                <p>Lorem ipsum dolor, sit amet consectetur adipisicing elit. Architecto fuga, magnam minus pariatur cumque ipsam velit rerum necessitatibus culpa, saepe perspiciatis! Dolores suscipit enim sed est quas autem minima repellat.</p>
            </div>
        </section>
        <section className='comment-container'>
            <textarea
              placeholder='Escreva um comentário'
              value={newComment}
              onChange={(e) => setNewComment(e.target.value)}
              rows={5}
            />
            <Button text='Enviar Comentario' type='env' url='' func={() => setTimeout}/>
        </section>
        <section className='comments-list'>
            {comments.map((comment, index) => (
                <Comment initialText={comment.text} onDelete={() => setComments(comments.filter((_, i) => i !== index))} key={index}/>
            ))}
        </section>
    </div>
  );
}

export default Ong;
