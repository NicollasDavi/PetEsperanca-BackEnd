import React, { useState } from 'react';
import './Comment.css';
import { CommentProps } from '../types/CommentProps';

const Comment = ({ initialText, onDelete } : CommentProps) => {
  const [isEditing, setIsEditing] = useState(false);
  const [text, setText] = useState(initialText);

  const handleEdit = () => {
    setIsEditing(true);
  };

  const handleSave = () => {
    setIsEditing(false);
  };

  return (
    <div className="comment">
      {isEditing ? (
        <textarea
          className="comment-edit"
          value={text}
          onChange={(e) => setText(e.target.value)}
        />
      ) : (
        <div className="comment-text">{text}</div>
      )}
      <div className="comment-buttons">
        {isEditing ? (
          <button className="buttonComment" onClick={handleSave}>Save</button>
        ) : (
          <>
            <button className="buttonComment" onClick={handleEdit}>Edit</button>
            <button className="buttonComment" onClick={onDelete}>Delete</button>
          </>
        )}
      </div>
    </div>
  );
};
export default Comment;
