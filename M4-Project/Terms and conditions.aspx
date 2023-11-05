<%@ Page Language="C#" Title="Terms and Condition" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="Terms and conditions.aspx.cs" Inherits="M4_Project.Terms_and_conditions" %>

<asp:Content ID ="HeadContent" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" type="text/css" href="/Content/terms_style.css">
</asp:Content>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div class="wrapper flex_align_justify">
        <div class="tc_wrap">
            <div class="tabs_list">
                    <h2>Terms & Conditions</h2>
                <ul>
                    <li data-tc="tab_item_1" class="active">Terms of use</li>
                    <li data-tc="tab_item_2">Intellectual property rights</li>
                    <li data-tc="tab_item_3">Prohibited activities</li>
                    <li data-tc="tab_item_4">Termination clause</li>
                    <li data-tc="tab_item_5">Governing law</li>
                    <li data-tc="tab_item_6">Cookie Usage</li>
                </ul>
            </div>
            <div class="tabs_content">
                <div class="tab_body">
                    <div class="tab_item tab_item_1">
                        <h3>Terms of use</h3>
                        <p>Friends and Family team can modify Terms & conditions at any time, in sole discretion. If Friends and Family team will be modifying any content, team will let you know either by site or through app. It's a major factor that you do agree to modified Terms & conditions. If you don't agree to be bound by the modified Terms, then you can't use the services any more. Over Srvices are evolving over time we can change or close any services at any time without any notice, at our sole discretion.</p>
                    </div>
                    <div class="tab_item tab_item_2" style="display: none;">
                        <h3>Intellectual property rights</h3>
                        <p>The content on the Website and the trademarks, service marks and logos contained therein (“Marks”) are owned by or licensed to Friends and Family, and are subject to copyright and other intellectual property rights under South African and foreign laws and international conventions. Friends and Family Content, includes, without limitation, all source code, databases, functionality, software, website designs, audio, video, text, photographs and graphics.</p>
                    </div>
                    <div class="tab_item tab_item_3" style="display: none;">
                        <h3>Prohibited activities</h3>
                        <p>Do not distribute content that harms or interferes with the operation of the networks,Servers, or other infrastructure of Friends and Family.</p>
                    </div>
                    <div class="tab_item tab_item_4" style="display: none;">
                        <h3>Termination clause</h3>
                        <p>Upon termination or suspension you must immediately destroy any douwnloaded or printed extracts from this website.</p>
                        <p>Your account will be terminated if you breach any other material terms of this website terms.</p>
                    </div>
                    <div class="tab_item tab_item_5" style="display: none;">
                        <h3>Governing law</h3>
                        <p>The website terms shall be governed by and construed in accordance with South African Law.</p>
                        <p>Disputes arising in connection with this website terms shall be subject to exclusive jurisdiction of the South African courts</p>
                    </div>
                    <div class="tab_item tab_item_6" style="display: none;">
                        <h3>Cookie Usage</h3>
                        <p>By using our website, you consent to the use of cookies in accordance with these terms. You can manage your cookie preferences through your browser settings. Please note that restricting cookies will impact the functionality of the website.</p>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>

<asp:Content ID="ContentScripts" ContentPlaceHolderID="ContentScripts" runat="server">
    <script type="text/javascript" src="Scripts/terms_Script.js"></script>
</asp:Content>
