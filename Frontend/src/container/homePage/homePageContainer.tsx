import React from 'react'
import ListingCarContainer from '../car/listingCarContainer'
import HeaderConatiner from '../header/headerContainer'


export default function HomePageContainer() {
    const [isAddNewCar,setNewCar]=React.useState(false);
    const [totalCount, setTotalCount] = React.useState(0);
    const [status, setStatus] = React.useState("");

    const onAddNew=()=>{
        setNewCar(true);
    }
    const onClose=()=>{
        setNewCar(false);
    }

    return (
        <div>
            <HeaderConatiner totalCount={totalCount}  onAddNew={onAddNew} setStatus={setStatus}/>
            <ListingCarContainer setTotalCount={setTotalCount}  isAddNewCar={isAddNewCar} onClose={onClose}  status={status} setStatus={setStatus}/>
        </div>
    )
}
