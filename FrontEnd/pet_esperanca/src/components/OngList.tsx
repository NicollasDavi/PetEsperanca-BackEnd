import React, {useEffect, useState} from 'react'
import OngCard from './OngCard'
import axiosInstance from '../services/axios/axiosInstance';

const OngList = () => {
    const [ongsList, setOngsList] = useState<{ongName : string, id: string}[]>([]);

    const fetchOngs = () => {
        axiosInstance.get('/list/ong').then((response) => {
            setOngsList(response.data)
        }).catch((error) => {
            console.log("Deu ruim:", error)
        })
    }

    useEffect(() => {
        fetchOngs()
    }, [])

    const handleDelete = async (id: string) => {
        await axiosInstance.delete( `/ong/delete/${id}`).then((repsonse) => {
            fetchOngs()
        }).catch((erro) => {
            console.log("Deu ruim", erro)
        })
    }


  return (
    <div>
        {ongsList.map((ong, id) => (
            <OngCard nome={ong.ongName} key={id} onDelete={() => handleDelete(ong.id)} imagemUrl=''/>
        ))}
    </div>
  )
}

export default OngList
