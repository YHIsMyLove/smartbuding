// 申请动火作业审批页面
<template>
    <f7-page>
        <f7-navbar title="申请动火作业" back-link="Back"></f7-navbar>
        <f7-list inline-labels no-hairlines-md>
        <f7-list-item>
            <f7-label>Name</f7-label>
            <f7-input type="text" placeholder="Your name" :value="value" clear-button></f7-input>
        </f7-list-item>

        <f7-list-item>
            <f7-label>Password</f7-label>
            <f7-input type="password" placeholder="Your password" clear-button></f7-input>
        </f7-list-item>

        <f7-list-item>
            <f7-label>E-mail</f7-label>
            <f7-input type="email" placeholder="Your e-mail" clear-button></f7-input>
        </f7-list-item>

        <f7-list-item>
            <f7-label>URL</f7-label>
            <f7-input type="url" placeholder="URL" clear-button></f7-input>
        </f7-list-item>

        <f7-list-item>
            <f7-label>Phone</f7-label>
            <f7-input type="tel" placeholder="Your phone number" clear-button></f7-input>
        </f7-list-item>

        <f7-list-item>
            <f7-label>Gender</f7-label>
            <f7-input type="select" placeholder="Please choose...">
                <option value="Male">Male</option>
                <option value="Female">Female</option>
            </f7-input>
        </f7-list-item>

        <f7-list-item>
            <f7-label>Birthday</f7-label>
            <f7-input type="date" value="2014-04-30" placeholder="Please choose..."></f7-input>
        </f7-list-item>

        <f7-list-item>
            <f7-label>Date time</f7-label>
            <f7-input type="datetime-local" placeholder="Please choose..."></f7-input>
        </f7-list-item>

        <f7-list-item>
            <f7-label>Range</f7-label>
            <f7-input type="range" value="50" min="0" max="100" step="1"></f7-input>
        </f7-list-item>

        <f7-list-item>
            <f7-label>Textarea</f7-label>
            <f7-input type="textarea" placeholder="Bio"></f7-input>
        </f7-list-item>

        <f7-list-item>
            <f7-label>Resizable</f7-label>
            <f7-input type="textarea" resizable placeholder="Bio"></f7-input>
        </f7-list-item>

        </f7-list>
    </f7-page>
</template>
