<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Terms and conditions.aspx.cs" Inherits="M4_Project.Terms_and_conditions" %>

<!DOCTYPE html>
<html>
<head>
    <title>Terms and Condition Section Tabs In HTML CSS and Javascript</title>
    <link rel="stylesheet" type="text/css" href="Content/terms_style.css">
</head>
<body>

    <div class="wrapper flex_align_justify">
        <div class="tc_wrap">
            <div class="tabs_list">
                <ul>
                    <li data-tc="tab_item_1" class="active">Terms of use</li>
                    <li data-tc="tab_item_2">Intellectual property rights</li>
                    <li data-tc="tab_item_3">Prohibited activities</li>
                    <li data-tc="tab_item_4">Termination clause</li>
                    <li data-tc="tab_item_5">Governing law</li>
                </ul>
            </div>
            <div class="tabs_content">
                <div class="tab_head">
                    <h2>Terms & Conditions</h2>
                </div>
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
                </div>
                <div class="tab_foot flex_align_justify">
                    <button class="decline">
                        Decline
                    </button>
                    <button class="agree">
                        Agree
                    </button>
                </div>
            </div>
        </div>
    </div>

    <script type="text/javascript" src="Scripts/terms_Script.js"></script>

</body>
</html>
