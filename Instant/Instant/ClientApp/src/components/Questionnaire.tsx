import React, {useEffect, useState} from 'react';
import axios from "axios";
import {EmploymentEnum} from "../enums/EmploymentEnum";
import {PurposeEnum} from "../enums/PurposeEnum";
import {DepositEnum} from "../enums/DepositEnum";
import {
    AdultMax,
    AdultMin, AmountMax, AmountMin,
    NameMaxLength,
    NameMinLength,
    PassportIssuerMaxLength,
    PassportIssuerMinLength,
    PassportNumberLength,
    PassportRegInformationMaxLength,
    PassportRegInformationMinLength,
    PassportSeriesLength,
    PatronymicMaxLength,
    PatronymicMinLength,
    SurnameMaxLength,
    SurnameMinLength
} from "../constants/Constants";
import {IsCreditDtoValid} from "../Validator";
import {Request} from "../entities/Request";
import "./Questionnaire.css"

export function Questionnaire() {
    const [name, setName] = useState("")
    const [surname, setSurname] = useState("")
    const [patronymic, setPatronymic] = useState("")
    const [passportSeries, setPassportSeries] = useState<string>("")
    const [passportNumber, setPassportNumber] = useState<string>("")
    const [passportIssuer, setPassportIssuer] = useState("")
    const [passportIssueDate, setPassportIssueDate] = useState<Date>(new Date())
    const [passportRegInformation, setPassportRegInformation] = useState("")
    const [adult, setAdult] = useState<number>(0)
    const [isJudged, setJudging] = useState<boolean>(false)
    const [employment, setEmployment] = useState<EmploymentEnum>(EmploymentEnum.ContractLaborCodeRusFed)
    const [purpose, setPurpose] = useState<PurposeEnum>(PurposeEnum.Consumer)
    const [deposit, setDeposit] = useState<DepositEnum>(DepositEnum.None)
    const [carAge, setCarAge] = useState<number>(0)
    const [hasOtherCredits, setOtherCredits] = useState<boolean>(false)
    const [amount, setAmount] = useState<number>(0)

    const submitForm = async (e: any) => {
        e.preventDefault()
        let errorLabel = document.getElementsByClassName("errorLabel")[0];
        errorLabel.innerHTML = "&nbsp;";
        let dto: Request = {
            name: name,
            surname: surname,
            patronymic: patronymic,
            passportSeries: passportSeries,
            passportNumber: passportNumber,
            passportIssuer: passportIssuer,
            passportIssueDate: passportIssueDate,
            passportRegInformation: passportRegInformation,
            adult: adult,
            isJudged: isJudged,
            employment: employment,
            purpose: purpose,
            deposit: deposit,
            carAge: carAge,
            hasOtherCredits: hasOtherCredits,
            amount: amount
        };
        let result = IsCreditDtoValid(dto)
        if (result === true) {
            axios.post("credit/take", dto)
                .then(r => errorLabel.innerHTML = r.data)
                .catch(r => console.log(r));
        } else
            errorLabel.innerHTML = result;
    }

    return (
        <div>
            <form>
                <h4 className='h4'>Personality</h4>
                    <div id='personality' className='row m-0'>
                        <div className='m-0 w-33' id='name'>
                            <label>Surname:</label>
                            <input className='border rounded ms-2 col-9 p-1'
                                   value={surname}
                                   minLength={SurnameMinLength}
                                   maxLength={SurnameMaxLength}
                                   onChange={(e) => setSurname(e.target.value)}/>
                        </div>
                        <div className='m-0 w-33' id='surname'>
                            <label>Name:</label>
                            <input className='border rounded ms-2 col-9 p-1'
                                   value={name}
                                   minLength={NameMinLength}
                                   maxLength={NameMaxLength}
                                   onChange={(e) => setName(e.target.value)}/> 
                        </div>
                        <div className='m-0 w-33' id='patronymic'>
                            <label>Patronymic:</label>
                            <input className='border rounded ms-2 col-9 p-1'
                                   value={patronymic}
                                   minLength={PatronymicMinLength}
                                   maxLength={PatronymicMaxLength}
                                   onChange={(e) => setPatronymic(e.target.value)}/>
                        </div>
                        <div className='mt-2 w-33'>
                            <label>Employment</label>
                            <select  className='border rounded ms-2 p-1'
                                    onChange={(e) => {
                                        let value = Number.parseInt(e.target.value);
                                        switch (value) {
                                            case 0:
                                                setEmployment(EmploymentEnum.ContractLaborCodeRusFed);
                                                break;
                                            case 1:
                                                setEmployment(EmploymentEnum.IndividualEntrepreneur);
                                                break;
                                            case 2:
                                                setEmployment(EmploymentEnum.Freelancer);
                                                break;
                                            case 3:
                                                setEmployment(EmploymentEnum.Retiree);
                                                break;
                                            case 4:
                                                setEmployment(EmploymentEnum.Unemployed);
                                                break;
                                        }
                                    }}>
                                <option value="0">Contract</option>
                                <option value="1">Individual</option>
                                <option value="2">Freelanser</option>
                                <option value="3">Pensioner</option>
                                <option value="4">Unemployed</option>
                            </select> 
                        </div>
                        <div className='mt-2 w-33'>
                            <label>Age:</label>
                            <input className='border rounded ms-2 p-1'
                                   value={adult}
                                   maxLength={3}
                                   onChange={(e) => {
                                       let result = Number.parseInt(e.target.value);
                                       if (!isNaN(result))
                                           setAdult(result)
                                       if (e.target.value === "")
                                           setAdult(0)
                                   }}/>
                        </div>
                    </div>


                <h4 className='h4'>Personality</h4>    
                    <div id='passport' className='row m-0'>
                        <div className='mt-2 w-33'>
                            <label>Series:</label>
                            <input  className='border rounded ms-2 p-1'
                                   value={passportSeries}
                                   maxLength={PassportSeriesLength}
                                   onChange={(e) => {
                                       let value = Number.parseInt(e.target.value);
                                       if (!isNaN(value) || e.target.value === "")
                                           setPassportSeries(e.target.value)
                                   }}/>
                        </div>

                        <div className='mt-2 w-33'>
                            <label>Number:</label>
                            <input className='border rounded ms-2 p-1'
                                   value={passportNumber}
                                   maxLength={PassportNumberLength}
                                   onChange={(e) => {
                                       let result = Number.parseInt(e.target.value);
                                       if (!isNaN(result) || e.target.value === "")
                                           setPassportNumber(e.target.value)
                                   }}/>
                        </div>
                        <div className='mt-2 w-33'>
                            <label>Issue date:</label>
                            <input className='border rounded ms-2 p-1'
                                   type="date"
                                   value={passportIssueDate.toISOString().split('T')[0]}
                                   min="1900-01-01"
                                   max="3000-01-01"
                                   onChange={(e) => {
                                       if (!isNaN(Date.parse(e.target.value)))
                                           setPassportIssueDate(new Date(e.target.value));
                                   }}/>
                        </div>
                        
                        <div className='mt-2'>
                            <label>Issued by:</label>
                            <input className='border rounded col-11 ms-2 p-1'
                                   value={passportIssuer}
                                   minLength={PassportIssuerMinLength}
                                   maxLength={PassportIssuerMaxLength}
                                   onChange={(e) => setPassportIssuer(e.target.value)}/>
                        </div>
                        
                        <div className='mt-2'>
                            <label>Registration:</label>
                            <input className='border rounded col-11 ms-2 p-1'
                                   minLength={PassportRegInformationMinLength}
                                   maxLength={PassportRegInformationMaxLength}
                                   value={passportRegInformation}
                                   onChange={(e) => setPassportRegInformation(e.target.value)}/>
                        </div>  
                    </div>
                
                <h4 className='h4'>Credit info</h4>
                <div className='row m-0'>
                    <div className='mt-2 w-33'>
                        <label>Purpose</label>
                        <select className='border rounded ms-2 p-1'
                                onChange={(e) => {
                                    let value = Number.parseInt(e.target.value);
                                    switch (value) {
                                        case 0:
                                            setPurpose(PurposeEnum.Consumer);
                                            break;
                                        case 1:
                                            setPurpose(PurposeEnum.Realty);
                                            break;
                                        case 2:
                                            setPurpose(PurposeEnum.Recrediting);
                                            break;
                                    }
                                }}>
                            <option value="0">Consumer</option>
                            <option value="1">Realty</option>
                            <option value="2">On-lending</option>
                        </select>
                    </div>
                    
                    <div className='mt-2 w-33'>
                        <label>Pledge</label>
                        <select className='border rounded ms-2 p-1'
                                onChange={(e) => {
                                    let value = Number.parseInt(e.target.value);
                                    switch (value) {
                                        case 0:
                                            setDeposit(DepositEnum.None);
                                            break;
                                        case 1:
                                            setDeposit(DepositEnum.Retiree);
                                            break;
                                        case 2:
                                            setDeposit(DepositEnum.Car);
                                            break;
                                        case 3:
                                            setDeposit(DepositEnum.Guarantee);
                                            break;
                                    }
                                }}>
                            <option value="0">None</option>
                            <option value="1">Realty</option>
                            <option value="2">Car</option>
                            <option value="3">Garantee</option>
                        </select>
                    </div>
                    
                    <div className='mt-2 w-33'>
                        <label>Car age(if car is pledge)</label>
                        <input className='border rounded ms-2 p-1'
                               value={carAge}
                               maxLength={2}
                               onChange={(e) => {
                                   let result = Number.parseInt(e.target.value);
                                   if (!isNaN(result))
                                       setCarAge(result)
                                   if (e.target.value == "")
                                       setCarAge(0)
                               }}/>
                    </div>
                    
                    <div className='mt-2'>
                        <label>Amount</label>
                        <input className='border rounded p-1'
                               value={amount}
                               maxLength={AmountMax.toString().length}
                               onChange={(e) => {
                                   let result = Number.parseInt(e.target.value);
                                   if (!isNaN(result))
                                       setAmount(result)
                                   if (e.target.value == "")
                                       setAmount(0)
                               }}/>
                    </div>
                    
                </div>
                    <label>Criminal record:</label>
                    <select className='border rounded ms-2 p-1'
                            onChange={(e) => {
                                if (e.target.value == "true")
                                    setJudging(true);
                                else
                                    setJudging(false);
                            }}>
                        <option value="false">Hasn't</option>
                        <option value="true">Has</option>
                    </select>

                    

                    <label>Other credits:</label>
                    <select className='border rounded ms-2 p-1'
                            onChange={(e) => {
                                if (e.target.value == "true")
                                    setOtherCredits(true);
                                else
                                    setOtherCredits(false);
                            }}>
                        <option value="false">Hasn't</option>
                        <option value="true">Has</option>
                    </select>
                
                    <label className="errorLabel">&nbsp;</label>
                    <button className="btn btn-primary justify-content-center my-2 col-12" onClick={submitForm}>Check</button>
            </form>
        </div>
    )
}
