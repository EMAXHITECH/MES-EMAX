# MES-EMAX

## This solution is a MES System with a function - data-gathering function.

### The above MES System records and manages all factory production activities as data, increasing productivity and enabling real-time communication between managers and field workers.
### And it is possible to automate the facilities by linking each other and check if there is any abnormalities in the facilities

#### 1. Description about the program main function.
   1) The administrator (or related worker) can register work order.
   ![WorkSheet](https://user-images.githubusercontent.com/109930235/192725384-2aec5926-a08f-458d-9c4e-8c8f7f5c0325.jpg)
   2) At work place - factory or etc. , workers can check work order informations in real time
   ![WorkResult](https://user-images.githubusercontent.com/109930235/192725963-e053ddee-6e79-41c5-bde6-68450daa3516.jpg)
   3) The operator records a work result at the work place
   ![WorkResult2](https://user-images.githubusercontent.com/109930235/192726157-54fd7a44-a169-4e6a-9a77-8e32dcbc4263.jpg)
   4) In the office, managers can search informations about work results.
   ![WorkResult3](https://user-images.githubusercontent.com/109930235/192726421-ccc51f37-c707-4403-a8d3-136bbba4453f.jpg)
   5) Establish facility linkage. <br />
   ![Equip](https://user-images.githubusercontent.com/109930235/192726598-6bf52c7a-7c68-4028-9fc5-49f4db65c6e6.jpg)
   6) It is possible to monitor field work in real time.<br />
    6-1) Production performance <br />
    ![Monitoring](https://user-images.githubusercontent.com/109930235/192726790-cd0e37fe-a3ac-4fc5-9f23-bba8ff76b62a.jpg) <br />
    6-2) Result compared to Order <br />
    ![Monitoring3](https://user-images.githubusercontent.com/109930235/192726926-7cfc5fcb-948d-44aa-8c08-edaa3e77e4b1.jpg)
    
#### 2. This is the basic specifications and preferences for using the above solution.
   1) Specification

    1-1) .NET 4.5.2 Or Over than .NET 4.5.2 
    1-2) Dev 20.2.12 Or Over than Dev 20.2.12
    1-3) Visual Studio 2017 Or Over than Visual Studio 2017
    1-4) Mssql 2016 Or Over than Mssql 2016
    
   2) Default Settings

    2-1) After downloading all these files, you need to create a database for this program.
    2-2) Please restore the database with the file - EMAX_DB.bak
    2-3) Set your server IP Address as the program server address.
         (Set the program server IP through configuration in login screen
          or edit server information in App.config of the solution)
