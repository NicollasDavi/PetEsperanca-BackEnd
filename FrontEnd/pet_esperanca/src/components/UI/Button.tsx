import React from 'react'
import { ButtonProps } from '../../types/ButtonProps'
import './button.css'

const Button = ({text, url, type, func} : ButtonProps) => {
  return (
    <>
     {type === 'env' &&
        <>
        <button onClick={func} className='button'>
            {text}
        </button>
        </>
    }
    {
        type === 'redirect' &&
        <>
        <a href={url} className='button'>
        {text}
        </a>
        </>
    }
    </>
   
  )
}

export default Button
