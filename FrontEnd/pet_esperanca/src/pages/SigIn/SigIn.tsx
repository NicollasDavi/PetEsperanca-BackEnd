import React, { useState } from 'react';
import './SignIn.css';
import axiosInstance from "../../services/axios/axiosInstance";
import { AxiosResponse } from 'axios';
import { useNavigate } from 'react-router-dom'; // Importe useNavigate do react-router-dom

interface User {
  Email: string;
  Senha: string;
}

interface NewUser {
  name: string;
  email: string;
  cpf: string;
  tel: string;
  senha: string;
}

const SignIn = () => {
  const [email, setEmail] = useState('');
  const [password, setPassword] = useState('');
  const [error, setError] = useState('');
  const [isCadastro, setIsCadastro] = useState(false);
  const [cpf, setCpf] = useState('');
  const [telefone, setTelefone] = useState('');
  const [nome, setNome] = useState('');
  
  // Inicialize o hook useNavigate
  const navigate = useNavigate();

  const handleSubmit = async (e: React.FormEvent<HTMLFormElement>) => {
    e.preventDefault();

    if (!email || !password) {
      setError('Por favor, preencha todos os campos.');
      setTimeout(() => {
        setError('');
      }, 4000);
      return;
    }

    try {
      if (isCadastro) {
        const newUser: NewUser = {
          name: nome,
          email: email,
          cpf: cpf,
          tel: telefone,
          senha: password
        };

        const response = await axiosInstance.post("/user", newUser);
        console.log(response.data);
        saveToLocalStorage(response.data);
      } else {
        const user: User = {
          Email: email,
          Senha: password
        };

        const response = await axiosInstance.post("/logar", user);
        console.log(response.data);
        saveToLocalStorage(response.data);
      }

      // Após salvar no localStorage, navegar para '/home'
      navigate('/home');

    } catch (error) {
      console.log(error);
      setError('Erro ao realizar o login ou cadastro.');
    }
  };

  const saveToLocalStorage = (data: any) => {
    localStorage.setItem('userData', JSON.stringify(data));
    console.log("Dados salvos no localStorage");
  };

  return (
    <div className='signin-container'>
      <form className='signin-form' onSubmit={handleSubmit}>
        <h1>Quase lá voluntário</h1>
        {error && <p className='error'>{error}</p>}
        {isCadastro &&
          <div className='form-group'>
            <label htmlFor="nome">Nome</label>
            <input type="text" name="nome" id="nome" value={nome} onChange={(e) => setNome(e.target.value)} />
          </div>
        }
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
          <>
            <div className='form-group'>
              <label htmlFor='cpf'>CPF:</label>
              <input
                type='text'
                id='cpf'
                value={cpf}
                onChange={(e) => setCpf(e.target.value)}
              />
            </div>
            <div className='form-group'>
              <label htmlFor='telefone'>Telefone:</label>
              <input
                type='text'
                id='telefone'
                value={telefone}
                onChange={(e) => setTelefone(e.target.value)}
              />
            </div>
          </>
        }
        <button type='submit' className='submit-button'>{!isCadastro ? "Sign In" : "Cadastrar"}</button>
        <p onClick={() => setIsCadastro(!isCadastro)}>{isCadastro ? "Já é um voluntário? Entrar" : "Não é um voluntário? Cadastre-se"}</p>
      </form>
    </div>
  );
};

export default SignIn;
