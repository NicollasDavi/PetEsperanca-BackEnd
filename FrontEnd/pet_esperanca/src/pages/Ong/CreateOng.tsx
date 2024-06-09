import React, { useEffect, useState } from 'react'
import Input from '../../components/UI/Input'
import Button from '../../components/UI/Button'
import  axiosInstance  from '../../services/axios/axiosInstance'
import './CreateOng.css'

const CreateOng = () => {
    const [ongName, setOngName] = useState("")
    const [cnpj, setCnpj] = useState("")
    const [erro, setErro] = useState("")

    const handleCreateOng = () => {
        if(ongName === "" || cnpj === ""){
            return setErro("Preenche os campos aí negão")
        }
        const ong = {
            ongName,
            cnpj
        }
        axiosInstance.post("/signin", ong).then((response) => {
            console.log(response)
        }).catch((error) => {
            console.log("Deu ruim", error)
        })
    }

    useEffect(() => {
        setTimeout(() => {
            setErro("")
        }, 3000)
    }, [erro])

  return (
    <div>
        <section>
            <Input placeholder='Nome da Ong' setState={setOngName} state={ongName} type='text'/>
            <Input placeholder='Cnpj da Ong' setState={setCnpj} state={cnpj} type='text'/>
        </section>
        <section>
            <Button text='Create Ong' url='/newong' type='env' func={handleCreateOng}/>
        </section>
        {erro !== "" && 
        <p className='erro'>{erro}</p>
        }
    </div>
  )
}

export default CreateOng
