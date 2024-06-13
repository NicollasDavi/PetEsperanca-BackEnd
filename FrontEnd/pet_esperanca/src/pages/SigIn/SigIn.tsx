import React, { useState } from 'react';
import './SignIn.css';

const SignIn = () => {
  const [email, setEmail] = useState('');
  const [password, setPassword] = useState('');
  const [error, setError] = useState('');
  const [isCadastro, setIsCadastro] = useState(false)
  const [cpf, setCpf] = useState("")

  const handleSubmit = (e: any) => {
    e.preventDefault();
    if (!email || !password) {
      setError('Por favor, preencha todos os campos.');
    } else {
      setError('');
      console.log('Email:', email);
      console.log('Password:', password);
    }
  };

  return (
    <div className='signin-container'>
      <form className='signin-form' onSubmit={handleSubmit}>
        <h1>Quase lá voluntario</h1>
        {error && <p className='error'>{error}</p>}
        <div className='form-group'>
          <label htmlFor='email'>Email:</label>
          <input
            type='email'
            id='email'
            value={email}
            onChange={(e) => setEmail(e.target.value)}
          />
        </div>
        <div className='form-group'>
          <label htmlFor='password'>Password:</label>
          <input
            type='password'
            id='password'
            value={password}
            onChange={(e) => setPassword(e.target.value)}
          />
        </div>
        {isCadastro && 
            <div className='form-group'>
            <label htmlFor='cpf'>CPF:</label>
            <input
              type='tect'
              id='cpf'
              value={cpf}
              onChange={(e) => setCpf(e.target.value)}
            />
          </div>
        }
        <button type='submit' className='submit-button'>Sign In</button>
        <p onClick={() => setIsCadastro(!isCadastro)}>{isCadastro ? "Já é um voluntario? Entrar" : "Não é um voluntario? Cadastre-se"}</p>

      </form>
      
    </div>
  );
};

export default SignIn;
