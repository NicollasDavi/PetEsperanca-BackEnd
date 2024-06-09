import React from 'react'
import { InputProps } from '../../types/InputProps'
import './input.css'

const Input = ({setState, state, type, placeholder} : InputProps) => {
  return (
    <div>
      <input type={type}
      value={state}
      placeholder={placeholder}
      onChange={(e) => setState(e.target.value)}
      /> 
    </div>
  )
}

export default Input
