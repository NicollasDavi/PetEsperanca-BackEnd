import React, { useEffect, useState } from 'react';
import { useParams } from 'react-router-dom';
import "./ongpage.css";
import Button from '../../components/UI/Button';
import Comment from '../../components/Comment';
import axiosInstance from "../../services/axios/axiosInstance";
import { OngInterface } from '../../types/OngInterface';
import { CommentInterface } from '../../types/CommentInterface';

const Ong = () => {
  const { id, action } = useParams<{ id: string; action: string }>();

  const [comments, setComments] = useState<CommentInterface[]>([]);
  const [comment, setComment] = useState<string>("");

  const [ongName, setOngName] = useState<string>('');
  const [sobre, setSobre] = useState<string>('');
  const [image, setImage] = useState<string | undefined>();
  const [imageBase64, setImageBase64] = useState<string>("");

  const handleFileChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    const file = e.target.files?.[0];
    if (file) {
      const reader = new FileReader();
      reader.onloadend = () => {
        setImageBase64(reader.result as string);
        setImage(reader.result as string);
      };
      reader.readAsDataURL(file);
    }
  };

  useEffect(() => {
    console.log(`Fetching ONG data for ID: ${id}, action: ${action}`);
    axiosInstance.get(`/ong/${id}`).then((response) => {
      const { ongName, sobre, image } = response.data;
      console.log('Received ONG data:', response.data);
      setOngName(ongName || '');
      setSobre(sobre || '');
      if (image && !image.startsWith('data:image')) {
        setImage(`data:image/jpeg;base64,${image}`);
      } else {
        setImage(image || undefined);
      }
    }).catch(error => {
      console.error('Erro ao buscar dados da ONG:', error);
    });
    
    fetchComments();
  }, [id, action]);

  const fetchComments = () => {
    axiosInstance.get(`/comments/${id}`).then((response) => {
      console.log('Received comments:', response.data);
      setComments(response.data);
    }).catch(error => {
      console.error('Erro ao buscar comentários:', error);
    });
  };

  const handleAddComment = () => {
    axiosInstance.post(`/comment/${id}`, { comment: comment }).then(() => {
      console.log('Comment added successfully');
      fetchComments();
      setComment("");
    }).catch(error => {
      console.error('Erro ao adicionar comentário:', error);
    });
  };

  const handleDelete = (commentId: string) => {
    axiosInstance.delete(`/comment/${commentId}`).then(() => {
      console.log(`Comment ${commentId} deleted successfully`);
      fetchComments();
    }).catch(error => {
      console.error('Erro ao deletar comentário:', error);
    });
  };

  const handleEditSubmit = () => {
    const dataToUpdate: Partial<OngInterface> = {
      ongName: ongName,
      sobre: sobre
    };

    if (imageBase64) {
      dataToUpdate.image = imageBase64;
    }

    axiosInstance.patch(`/ong/update/${id}`, dataToUpdate)
      .then(() => {
        console.log('Edit submitted successfully');
        axiosInstance.get(`/ong/${id}`).then((response) => {
          const { ongName, sobre, image } = response.data;
          console.log('Received updated ONG data:', response.data);
          setOngName(ongName || '');
          setSobre(sobre || '');
          if (image && !image.startsWith('data:image')) {
            setImage(`data:image/jpeg;base64,${image}`);
          } else {
            setImage(image || undefined);
          }
        }).catch(error => {
          console.error('Erro ao buscar dados atualizados da ONG:', error);
        });
      }).catch(error => {
        console.error('Erro ao atualizar dados da ONG:', error);
      });
  };

  const handleInputChange = (e: React.ChangeEvent<HTMLInputElement | HTMLTextAreaElement>) => {
    const { name, value } = e.target;
    if (name === 'ongName') {
      setOngName(value);
    } else if (name === 'sobre') {
      setSobre(value);
    }
  };

  return (
    <div>
      <section className='ong-apresentation'>
        <div className='ong-image-container'>
          {image && <img src={image} alt={ongName} />}
          {action === '1' && (
            <label className="image-upload-label">
              <input type="file" accept="image/*" onChange={handleFileChange} style={{ display: 'none' }} />
              Alterar Imagem
            </label>
          )}
        </div>
        <div className='ong-description'>
          <h1>{action === '1' ? <input type="text" name="ongName" value={ongName} onChange={handleInputChange} /> : ongName}</h1>
          <p>{action === '1' ? <textarea name="sobre" value={sobre} onChange={handleInputChange} /> : sobre}</p>
          {action === '1' && <Button text='Salvar Alterações' type='env' url='' func={handleEditSubmit} />}
        </div>
      </section>
      <section className='comment-container'>
        <textarea
          placeholder='Escreva um comentário'
          rows={5}
          onChange={(e) => setComment(e.target.value)}
          value={comment}
        />
        <Button text='Enviar Comentário' type='env' url='' func={handleAddComment} />
      </section>
      <section className='comments-list'>
        {comments.map((comment, index) => (
          <Comment initialText={comment.comment} onDelete={() => handleDelete(comment.id)} key={index} />
        ))}
      </section>
    </div>
  );
}

export default Ong;
