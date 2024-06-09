import React from 'react'
import OngList from '../../components/OngList'
import Button from '../../components/UI/Button'

const Ongs = () => {
  return (
    <div>
    <OngList/>
    <Button text='Create Ong' url='/newong' type='redirect'/>
    </div>
  )
}

export default Ongs
