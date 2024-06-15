import React, { useState } from 'react';
import './SignIn.css';
import axiosInstance from "../../services/axios/axiosInstance"

const SignIn = () => {
  const [email, setEmail] = useState('');
  const [password, setPassword] = useState('');
  const [error, setError] = useState('');
  const [isCadastro, setIsCadastro] = useState(false)
  const [cpf, setCpf] = useState("")
  const [telefone, setTelefone ] = useState("")
  const [nome, setNome] = useState("")

  const handleSubmit = (e: any) => {
    if (!email || !password) {
      setTimeout(() => {
        setError('Por favor, preencha todos os campos.');
      }, 4000)
      setError('');
    } 
    e.preventDefault();
    if(isCadastro){
      const newUSer = {
        Name: nome,
        Email: email,
        Cpf: cpf,
        Tel: telefone,
        Senha: password
      }
      axiosInstance.post("/sigin", newUSer).then((response) => {

      }).catch((error) => {

      })
    }else{
      const user = {
        Email: email,
        Senha: password
      }

      axiosInstance.post("/logar", user).then((response) => {

      }).catch((error) => {

      })
    }
   
  };

  return (
    <div className='signin-container'>
      <form className='signin-form' onSubmit={handleSubmit}>
        <h1>Quase lá voluntario</h1>
        {error && <p className='error'>{error}</p>}
        {isCadastro &&
        <div className='form-group'>
          <label htmlFor="nome">Nome</label>  
          <input type="text" name="" id="" onChange={(e) => setNome(e.target.value)}/>
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
          <label htmlFor='Tel'>Telefone:</label>
          <input
            type='telefone'
            id='telefone'
            value={telefone}
            onChange={(e) => setTelefone(e.target.value)}
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
        <button type='submit' className='submit-button'>{!isCadastro ? "Sign In" : "Cadastrar"}</button>
        <p onClick={() => setIsCadastro(!isCadastro)}>{isCadastro ? "Já é um voluntario? Entrar" : "Não é um voluntario? Cadastre-se"}</p>

      </form>
      
    </div>
  );
};

export default SignIn;
