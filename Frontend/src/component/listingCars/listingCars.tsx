import React, { Dispatch, SetStateAction, useState } from 'react'
import Car from './car/carCard'
import FilterCar from './filterCar/filterCar'
import classes from "./listingCars.module.scss"

import CarCard from './car/carCard'

type Props = {
    cars: any[];
      handleOpenUpdateModal: (car: any) => void;
    handleDelete: (car: any) => void;
}


export default function ListingCars({ cars,  handleOpenUpdateModal, handleDelete }: Props) {

    return (
        <div>
            <div className={classes.lisitng_container}>
                <div className={classes.listing}>
                    <div className={classes.car_display_div}>
                        {cars?.map(car => {
                            return <CarCard car={car} handleOpenUpdateModal={handleOpenUpdateModal} handleDelete={handleDelete} />
                        })}
                      
                    </div>
                </div>


            </div>
        </div>
    )
}
