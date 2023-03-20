import React, { useState } from 'react'
import classes from "./car.module.scss"
import EditImg from "../../../asset/images/edit.png"
import DeleteImg from "../../../asset/images/delete.png"
import {carSvg} from '../../../asset/commonSvg';


type Props = {
	car: any;
	handleOpenUpdateModal: (car: any) => void;
	handleDelete: (car: any) => void;
}

export default function CarCard({ car, handleOpenUpdateModal, handleDelete }: Props) {
	const [style, setStyle] = useState({display: 'none'});
	return (
		<div className={classes.car} onMouseEnter={e => {
			setStyle({display: 'flex'});
		}}
		onMouseLeave={e => {
			setStyle({display: 'none'})
		}}>
			<div className={classes.car_container}>

				<div className={classes.detail_div}>

					<div className={classes.makename_div}>
						<div>{car.makeName} </div>
						<div> {car.modelName}</div>
					</div>
					<div className={classes.year_div}>{car.year}</div>
					<div className={classes.price_div}>
						₹{car.leftCurrentValue} <span>{`( ₹${car.currentValue} )`}</span>
					</div>

				</div>

				<div className={classes.image_div}>
					<div>
						<span style={{fill:car.colour}}>{carSvg}</span>
					</div>
				</div>
			</div>
			<div className={classes.edit_delete_div} style={style}>
				<div onClick={() => handleOpenUpdateModal(car)}><img src={EditImg} /></div>
				<div onClick={() => handleDelete(car)}><img src={DeleteImg} /></div>
			</div>

		</div>
	)
}
