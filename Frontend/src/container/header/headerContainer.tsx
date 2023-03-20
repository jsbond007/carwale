import React, {  Dispatch, SetStateAction } from 'react'
import Header from '../../component/header/Header'


type AddEditModalProps = {
  onAddNew: () => void,
  totalCount: number,
  setStatus:  Dispatch<SetStateAction<string>>,

}

export default function HeaderConatiner({totalCount, onAddNew ,setStatus}: AddEditModalProps) {
  return (
    <div>
      <Header onAddNew={onAddNew} totalCount={totalCount} setStatus={setStatus}/>
    </div>
  )
}
