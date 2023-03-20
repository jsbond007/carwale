import { Button, FormControl, FormLabel, Input, Modal, Select, Typography, MenuItem } from "@mui/material";
import { Stack } from "@mui/system";
import { ReactComponent as CarPicture } from '../../../asset/images/car.svg'
import { HexColorPicker } from 'react-colorful'
import classes from "./carModal.module.scss"

type AddEditModalProps = {
    carViewModel: {
        UId: string,
        Year: number,
        RegistrationNumber: string,
        Colour: string,
        CurrentValue: number,
        Notes: string,
        ModelUId: string,

    }
    handleInputForAdd: (e: React.ChangeEvent<HTMLInputElement | HTMLTextAreaElement>) => void
    submitCar: (uid: string) => Promise<void>
    onChangeSelect: (value: any) => void
    onColorChange: (value: any) => void
    models: any[];
    modelDDO: any[];
    onChangeSelectCarModel: (value: any) => void,
    selctedModel: string;
    selectedCarMark: string
    onCloseModal: () => void,
    statusList: any[],
    selectedStatus: number,
    onChangeSelectStatus: (value: any) => void,
}


export function CarModal({ carViewModel, models, handleInputForAdd, submitCar, onChangeSelect, onColorChange, modelDDO, onChangeSelectCarModel, selctedModel, selectedCarMark, onCloseModal, statusList, selectedStatus, onChangeSelectStatus }: AddEditModalProps) {
    return (
        <>
            <Typography id="basic-modal-dialog-title" component="h2">
                {carViewModel.UId && carViewModel.UId.length > 0
                    ? <h3>Edit Existing Car</h3> : <h3>Adding New Car</h3>
                }
            </Typography>

            <Typography id="basic-modal-dialog-description">
                Fill in the information of the car.
            </Typography>
            <Stack spacing={2}>

                <FormControl>
                    <FormLabel>Make</FormLabel>
                    <Select onChange={(e) => onChangeSelect(e)} value={selectedCarMark} >
                        {models?.map((item: any) => {
                            return (<MenuItem value={item.uId}>
                                {item.name}
                            </MenuItem>)
                        })}
                    </Select>
                </FormControl>

                <FormControl>
                    <FormLabel>Model</FormLabel>
                    <Select onChange={(e) => onChangeSelectCarModel(e)} value={selctedModel}>
                        {modelDDO?.map((item: any) => {
                            return (<MenuItem value={item.uId}>{item.name}</MenuItem>)
                        })}
                    </Select>
                </FormControl>
                {
                    carViewModel.UId && carViewModel.UId.length > 0 && <FormControl>
                        <FormLabel>Status</FormLabel>
                        <Select onChange={(e) => onChangeSelectStatus(e)} value={selectedStatus}>
                            {statusList?.map((item: any) => {
                                return (<MenuItem value={item.id}>{item.name}</MenuItem>)
                            })}
                        </Select>
                    </FormControl>
                }


                <Stack direction="row"
                    spacing={{ xs: 1, sm: 2, md: 4 }}
                >
                    <FormControl>
                        <FormLabel>Year</FormLabel>
                        <Input autoFocus type="number" name='Year' required value={carViewModel.Year} onChange={(e) => handleInputForAdd(e)} />
                    </FormControl>

                    <FormControl>
                        <FormLabel>Registration Number</FormLabel>
                        <Input required name="RegistrationNumber" value={carViewModel.RegistrationNumber} onChange={(e) => handleInputForAdd(e)} />
                    </FormControl>



                    <FormControl>
                        <FormLabel>Current Value</FormLabel>
                        <Input required type="number" name="CurrentValue" value={carViewModel.CurrentValue} onChange={(e) => handleInputForAdd(e)} />
                    </FormControl>
                </Stack>
                <Stack direction="row"
                    spacing={{ xs: 1, sm: 2, md: 4 }}
                >
                    <FormControl>
                        <FormLabel >Colour</FormLabel>
                        {/* <Input required name="Colour" value={color} onChange={(e) => handleInputForAdd(e)} /> */}
                        {/* <ColorPicker value={color} onChange={newcolor => { console.log(newcolor); setColor(newcolor.hex) }} />; */}
                        <CarPicture fill={carViewModel.Colour} width="200" height="150" ></CarPicture>

                    </FormControl>

                    <HexColorPicker color={carViewModel.Colour} onChange={onColorChange}></HexColorPicker>

                </Stack>

                <FormControl>
                    <FormLabel>Notes</FormLabel>
                    <Input required name="Notes" value={carViewModel.Notes} onChange={(e) => handleInputForAdd(e)} />
                </FormControl>

                <div className={classes.btn}>
                    <Button type="submit" onClick={() => submitCar(carViewModel.UId)} className={classes.saveBtn}>{carViewModel.UId ? "Update" : "Add"}</Button>
                    <Button onClick={() => onCloseModal()} className={classes.cancelBtn} >{"Cancel"}</Button>
                </div>
            </Stack>

        </>
    )
}