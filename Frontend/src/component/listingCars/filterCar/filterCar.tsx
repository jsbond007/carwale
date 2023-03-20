import React from 'react'
import classes from "./filterCar.module.scss"
import DownArrow from "../../../asset/images/down_arrow.png"

export default function FilterCar() {
  return (
    <div>
        <div className={classes.filter_container}>
            <div className={classes.filter_header}>
                <p>Filter</p>
                <p>Clear</p>
            </div>

            <div className={classes.filter_dropdown_div}>
                <h3 className={classes.sub_name}>
                    <div>Select City</div>
                    <span><img className={classes.down_img} src={DownArrow}/></span>
                </h3>
            </div>

            <div className={classes.filter_dropdown_div}>
                <h3 className={classes.sub_name}>
                    <div>Budget</div>
                    <span><img className={classes.down_img} src={DownArrow}/></span>
                </h3>
            </div>

            <div className={classes.filter_dropdown_div}>
                <h3 className={classes.sub_name}>
                    <div>Show Cars With</div>
                    <span><img className={classes.down_img} src={DownArrow}/></span>
                </h3>
            </div>

            <div className={classes.filter_dropdown_div}>
                <h3 className={classes.sub_name}>
                    <div>Manufacturer</div>
                    <span><img className={classes.down_img} src={DownArrow}/></span>
                </h3>
            </div>

            <div className={classes.filter_dropdown_div}>
                <h3 className={classes.sub_name}>
                    <div>Fuel Type</div>
                    <span><img className={classes.down_img} src={DownArrow}/></span>
                </h3>
            </div>

            <div className={classes.filter_dropdown_div}>
                <h3 className={classes.sub_name}>
                    <div>Colours</div>
                    <span><img className={classes.down_img} src={DownArrow}/></span>
                </h3>
            </div>

            <div className={classes.filter_dropdown_div}>
                <h3 className={classes.sub_name}>
                    <div>Body Type</div>
                    <span><img className={classes.down_img} src={DownArrow}/></span>
                </h3>
            </div>
            
        </div>
    </div>
  )
}
