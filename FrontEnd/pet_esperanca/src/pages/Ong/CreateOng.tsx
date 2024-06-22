import React, { useEffect, useState } from 'react';
import Input from '../../components/UI/Input';
import Button from '../../components/UI/Button';
import axiosInstance from '../../services/axios/axiosInstance';
import './CreateOng.css';

const CreateOng = () => {
  const [ongName, setOngName] = useState("");
  const [cnpj, setCnpj] = useState("");
  const [erro, setErro] = useState("");
  const [sobre, setSobre] = useState("");
  const [image, setImage] = useState<File | null>(null);
  const [imageBase64, setImageBase64] = useState("");

  const handleFileChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    const file = e.target.files?.[0];
    if (file) {
      setImage(file);
      const reader = new FileReader();
      reader.onloadend = () => {
        setImageBase64(reader.result as string);
      };
      reader.readAsDataURL(file);
    }
  };

  const handleCreateOng = () => {
    if (ongName === "" || cnpj === "") {
      return setErro("Preencha os campos obrigatórios");
    }
    const ong = {
      ongName,
      cnpj,
      sobre,
      image: imageBase64
    };
    axiosInstance.post("/ong", ong).then((response) => {
      console.log(response);
    }).catch((error) => {
      console.log("Deu ruim", error);
    });
  };

  useEffect(() => {
    const timer = setTimeout(() => {
      setErro("");
    }, 3000);
    return () => clearTimeout(timer);
  }, [erro]);

  return (
    <div className='container'>
      <section>
        <input type='file' onChange={handleFileChange} />
        <Input placeholder='Nome da Ong' setState={setOngName} state={ongName} type='text' />
        <Input placeholder='CNPJ da Ong' setState={setCnpj} state={cnpj} type='text' />
        <textarea placeholder='Sobre a Ong' onChange={(e) => setSobre(e.target.value)} />
      </section>
      <section>
        <Button text='Criar Ong' url='/newong' type='env' func={handleCreateOng} />
      </section>
      {erro !== "" &&
        <p className='erro'>{erro}</p>
      }
    </div>
  );
}

export default CreateOng;
