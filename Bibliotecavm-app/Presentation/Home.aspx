<%@ Page Title="Inicio" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="Home.aspx.cs" Inherits="Presentation.Home" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h1 class="welcome-title">Bienvenido</h1>
    
    <div class="slider-container">
        <div class="slider-track">
             <%--Primera serie de imágenes--%> 
            <div class="slider-item">
                <img src="Recursos/Materiales/Cartilla1.webp" alt="Cartilla 1">
            </div>
            <div class="slider-item">
                <img src="Recursos/Materiales/Cartilla2.webp" alt="Cartilla 2">
            </div>
            <div class="slider-item">
                <img src="Recursos/Materiales/Cartilla3.webp" alt="Cartilla 3">
            </div>
            <div class="slider-item">
                <img src="Recursos/Materiales/Cartilla4.webp" alt="Cartilla 4">
            </div>
            <div class="slider-item">
                <img src="Recursos/Materiales/Cartilla5.webp" alt="Cartilla 5">
            </div>
            <div class="slider-item">
                <img src="Recursos/Materiales/Cartilla6.webp" alt="Cartilla 6">
            </div>
            <div class="slider-item">
                <img src="Recursos/Materiales/Cartilla7.webp" alt="Cartilla 7">
            </div>
            <div class="slider-item">
                <img src="Recursos/Materiales/Cartilla8.webp" alt="Cartilla 8">
            </div>
            
             <%--Segunda serie de imágenes (duplicadas para efecto continuo)--%> 
            <div class="slider-item">
                <img src="Recursos/Materiales/Cartilla1.webp" alt="Cartilla 1">
            </div>
            <div class="slider-item">
                <img src="Recursos/Materiales/Cartilla2.webp" alt="Cartilla 2">
            </div>
            <div class="slider-item">
                <img src="Recursos/Materiales/Cartilla3.webp" alt="Cartilla 3">
            </div>
            <div class="slider-item">
                <img src="Recursos/Materiales/Cartilla4.webp" alt="Cartilla 4">
            </div>
            <div class="slider-item">
                <img src="Recursos/Materiales/Cartilla5.webp" alt="Cartilla 5">
            </div>
            <div class="slider-item">
                <img src="Recursos/Materiales/Cartilla6.webp" alt="Cartilla 6">
            </div>
            <div class="slider-item">
                <img src="Recursos/Materiales/Cartilla7.webp" alt="Cartilla 7">
            </div>
            <div class="slider-item">
                <img src="Recursos/Materiales/Cartilla8.webp" alt="Cartilla 8">
            </div>
        </div>
    </div>

        <style>
        .slider-container {
            width: 100%;
            overflow: hidden;
            position: relative;
            margin: 30px 0;
        }
        
        .slider-track {
            display: flex;
            animation: scroll 20s linear infinite;
            width: calc(200px * 16); /* Ancho total de todas las imágenes duplicadas */
        }
        
        .slider-item {
            width: 200px;
            height: 150px;
            flex-shrink: 0;
            padding: 0 10px;
        }
        
        .slider-item img {
            width: 100%;
            height: 100%;
            object-fit: cover;
            border-radius: 8px;
            box-shadow: 0 4px 8px rgba(0,0,0,0.2);
        }
        
        @keyframes scroll {
            0% { transform: translateX(0); }
            100% { transform: translateX(calc(-200px * 8)); }
        }
        
        h1.welcome-title {
            text-align: center;
            color: #2c3e50;
            margin-top: 20px;
            font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
        }
    </style>
</asp:Content>