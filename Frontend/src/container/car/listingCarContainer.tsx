import React, { useState, useEffect, Dispatch, SetStateAction } from 'react'
import ListingCars from '../../component/listingCars/listingCars'
import { AddCar, DeleteCar, Get, GetAll, UpdateCar } from '../../service/carService'
import { getCarMakeList, getCarModel } from '../../service/modelService'
import { toast } from "react-toastify";
import { useNavigate } from 'react-router-dom';
import Modal from '@mui/joy/Modal'
import ModalDialog from '@mui/joy/ModalDialog'
import Typography from '@mui/joy/Typography'
import Button from '@mui/joy/Button'
import { Divider } from '@mui/material'
import { Box } from '@mui/system'
import WarningRoundedIcon from '@mui/icons-material/WarningRounded';
import { CarModal } from '../../component/listingCars/car/carModal'
import { StringifyErrors } from '../../utils/ErrorHelper';

type AddEditModalProps = {
    isAddNewCar: Boolean;
    onClose: () => void;
    setTotalCount: Dispatch<SetStateAction<number>>,
    status: string,
    setStatus: Dispatch<SetStateAction<string>>,
}



export default function ListingCarContainer({ isAddNewCar, onClose, setTotalCount, status, setStatus }: AddEditModalProps) {
    const [cars, setCars] = useState<any>();
    const [models, setModels] = useState<any>();
    const [showModal, setShowModal] = useState(false);
    const [carModel, setCarModel] = useState({
        UId: "",
        Year: 0,
        RegistrationNumber: "",
        Colour: "",
        CurrentValue: 0,
        Notes: "",
        ModelUId: "",
        status: 0,

    });
    const [statusList, setStatusList] = useState([{
        id: 0,
        name: "Available"
    },
    {
        id: 1,
        name: "Unavailable"
    },
    {
        id: 2,
        name: "Stolen"
    },
    {
        id: 3,
        name: "Disposed"
    }
    ]);

    const [carMake, setCarMake] = useState({
        UId: "",
        makeName: "",
        createdDateTime: "",
        createdBy: "",
    });

    const [openDeleteModal, setOpenDeleteModal] = React.useState(false);
    const [modelDDO, setModelDDO] = useState<any>();
    const [selctedModel, setSelectedModel] = useState<string>("");
    const [selectedCarMark, setSelectedCarMark] = useState<string>("");
    const [selectedCarUId, setSelectedCarUId] = useState<string>("");
    const [selectedStatus, setSelectedStatus] = useState<number>(0);


    const navigate = useNavigate();

    useEffect(() => {
        if (!localStorage.getItem("token")) {
            navigate("/login");
        }
        getCarsData();
        getCarMakeListDDO();
    }, [])

    useEffect(() => {
        if (isAddNewCar) {
            setShowModal(true);
            setCarModel({
                UId: "",
                Year: 0,
                RegistrationNumber: "",
                Colour: "",
                CurrentValue: 0,
                Notes: "",
                ModelUId: "",
                status: 0,
            });
            setShowModal(true);
            setSelectedModel("");
            setSelectedCarMark("");
        }
    }, [isAddNewCar])

    useEffect(() => {
        if (status?.length > 0) {
            getCarsData();
        }
    }, [status])

    const getCarMakeListDDO = async () => {
        const res = await getCarMakeList();
        if (res) {
            let arr: any[] = [];
            arr = res.data.data.map((item: any) => {
                return {
                    uId: item.uId,
                    //change here 
                    name: item.makeName
                }
            })
            setModels(arr);
        }
    };

    const getCarModelDDO = async (makeUId: string, modelUId: string) => {
        const res = await getCarModel(makeUId);
        if (res) {
            let arr: any[] = [];
            arr = res.data.data.map((item: any) => {
                return {
                    uId: item.uId,
                    name: item.name
                }
            })
            setModelDDO(arr);
            if (arr?.length > 0) {
                setCarModel({ ...carModel, ModelUId: arr[0].uId })
                setSelectedModel(modelUId?.length > 0 ? modelUId : arr[0].uId)
            }
        }
    };

    const getCarsData = async () => {
        let statusValue = status !== "All" && status.length ? Number(status) : undefined;
        const res = await GetAll(statusValue);
        if (res) {
            let arr: any[] = [];
            arr = res.data?.data?.map((item: any) => {
                return {
                    modelName: item.modelName,
                    makeName: item.makeName,
                    registrationNumber: item.registrationNumber,
                    currentValue: item.currentValue,
                    leftCurrentValue: item.leftCurrentValue,
                    status: item.status,
                    year: item.year,
                    colour: item.colour,
                    uId: item.uId
                }
            })
            setCars(arr);
            setTotalCount(res.data?.recordAffected ? res.data.recordAffected : 0);
        }
    };

    const getCarData = async (uid: string) => {
        const res = await Get(uid);
        if (res && res.data) {
            const data = res.data.data;
            carModel.UId = data.uId;
            carModel.Year = data.year;
            carModel.RegistrationNumber = data.registrationNumber;
            carModel.Colour = data.colour;
            carModel.CurrentValue = data.currentValue;
            carModel.Notes = data.notes;
            carModel.ModelUId = data.modelUId;
            setCarModel(carModel);
            setSelectedCarMark(data.makeUId);
            getCarModelDDO(data.makeUId, data.modelUId);
            setSelectedStatus(data.status);
        }
    };

    const handleInputForAdd = (e: React.ChangeEvent<HTMLInputElement | HTMLTextAreaElement>) => {
        setCarModel({ ...carModel, [e.target.name]: `${e.target.value}`, });
    };

    const onChangeSelectCarMake = (value: any) => {
        setCarMake({ ...carMake, UId: value.target.value })
        setSelectedCarMark(value.target.value);
        getCarModelDDO(value.target.value, "");
    };

    const onChangeSelectCarModel = (value: any) => {
        setCarModel({ ...carModel, ModelUId: value.target.value })
        setSelectedModel(value.target.value);
    };

    const onChangeSelectStatus = (value: any) => {
        setSelectedStatus(value.target.value);
    };


    const onColorChange = (value: any) => {
        setCarModel({ ...carModel, Colour: value })
    }

    const submitCar = async () => {
        if (carModel.UId && carModel.UId.length > 0) {
            updateCar(carModel.UId);
        } else {
            addCar();
        }
    }

    const addCar = async () => {
        let stringNmber: string = carModel.Year.toString();
        let numberValue: number = +stringNmber;
        let stringNmber2: string = carModel.CurrentValue.toString();
        let numberValue2: number = +stringNmber2;
        carModel.Year = numberValue;
        carModel.CurrentValue = numberValue2;

        try {
            const res = await AddCar(carModel);
            if (res.data.data) {
                // setStatus("")
                setShowModal(false);
                onClose();
                toast.success("Car added successfully");
                getCarsData();
            }
        } catch (err: any) {
            const allErrors = StringifyErrors(err.response)
            toast(<div dangerouslySetInnerHTML={{ __html: allErrors }}></div>);
        }
    };

    const updateCar = async (uid: string) => {
        let stringNmber: string = carModel.Year.toString();
        let numberValue: number = +stringNmber;
        let stringNmber2: string = carModel.CurrentValue.toString();
        let numberValue2: number = +stringNmber2;
        carModel.Year = numberValue;
        carModel.CurrentValue = numberValue2;
        carModel.UId = uid;
        carModel.status = selectedStatus

        try {
            const res = await UpdateCar(carModel);
            if (res) {
                // setStatus("")
                toast.success("Car updated successfully");
                getCarsData();
                setShowModal(false);
                onClose();
            }
        }
        catch (err: any) {
            const allErrors = StringifyErrors(err.response)
            toast(<div dangerouslySetInnerHTML={{ __html: allErrors }}></div>);
        }
    };

    const deleteCar = async () => {
        try {
            const res = await DeleteCar(selectedCarUId);
            if (res) {
                // setStatus("")
                getCarsData();
                toast.success("Car deleted successfully");
            }
        }
        catch (err: any) {
            const allErrors = StringifyErrors(err.response)
            toast(<div dangerouslySetInnerHTML={{ __html: allErrors }}></div>);
        }
    }


    const handleOpenUpdateModal = async (car: any) => {
        await getCarData(car.uId);
        setShowModal(true);
    }

    const handleDelete = async (car: any) => {
        setSelectedCarUId(car.uId
        )
        // setSelectedCar(car);
        setOpenDeleteModal(true);
    }


    const onCloseModal = async () => {
        setShowModal(false)
        onClose();
    }

    return (
        <div>
            <ListingCars cars={cars}

                handleOpenUpdateModal={handleOpenUpdateModal}
                handleDelete={handleDelete}
            />

            <Modal open={showModal} onClose={() => onCloseModal()}>
                <ModalDialog
                    aria-labelledby="basic-modal-dialog-title"
                    aria-describedby="basic-modal-dialog-description"
                    sx={{ maxWidth: 500, minWidth: 750 }}
                    style={{ overflow: "auto" }}
                >
                    <CarModal carViewModel={carModel}
                        handleInputForAdd={handleInputForAdd}
                        models={models}
                        onChangeSelect={onChangeSelectCarMake}
                        onColorChange={onColorChange}
                        submitCar={submitCar}
                        modelDDO={modelDDO}
                        onChangeSelectCarModel={onChangeSelectCarModel}
                        selctedModel={selctedModel}
                        selectedCarMark={selectedCarMark}
                        onCloseModal={onCloseModal}
                        statusList={statusList}
                        selectedStatus={selectedStatus}
                        onChangeSelectStatus={onChangeSelectStatus}
                    />
                </ModalDialog>
            </Modal>

            <Modal open={openDeleteModal}
                onClose={() => setOpenDeleteModal(false)}
            >
                <ModalDialog
                    variant="outlined"
                    role="alertdialog"
                    aria-labelledby="alert-dialog-modal-title"
                    aria-describedby="alert-dialog-modal-description"
                >
                    <Typography
                        id="alert-dialog-modal-title"
                        component="h2"
                        startDecorator={<WarningRoundedIcon />}
                    >
                        Confirmation
                    </Typography>

                    <Divider />

                    <Typography id="alert-dialog-modal-description" textColor="text.tertiary">
                        Are you sure you want to delete this car?
                    </Typography>
                    <Box sx={{ display: 'flex', gap: 1, justifyContent: 'flex-end', pt: 2 }}>
                        <Button variant="plain" color="neutral" onClick={() => setOpenDeleteModal(false)}>
                            Cancel
                        </Button>
                        <Button variant="solid" color="danger" onClick={() => { deleteCar(); setOpenDeleteModal(false); }}>
                            Delete
                        </Button>
                    </Box>
                </ModalDialog>
            </Modal>

        </div>
    )
}
