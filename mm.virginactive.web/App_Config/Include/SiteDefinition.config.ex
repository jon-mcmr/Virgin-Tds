<!--

Purpose: This include file adds a new site definition

To enable this, rename this file so that it has a ".config" extension and 
change all the parameters to suit your own scenario

Notice how "patch:before" is used to insert the site definition BEFORE the 
existing <site name="website" ...> element 

You can use "patch:before" and "patch:after" as an attribute of an inserted 
element to specify an insertion point for the new element. Both accept an 
XPath relative to the parent node of the inserted element.

-->
<configuration xmlns:patch="http://www.sitecore.net/xmlconfig/">
  <sitecore>
    <sites>
      <site name="dev.virginactive" patch:before="site[@name='website']"
            virtualFolder="/"
            physicalFolder="/"
            rootPath="/sitecore/content"
            startItem="/home"
            database="master"
            domain="extranet"
            allowDebug="true"
            cacheHtml="true"
            htmlCacheSize="10MB"
            enablePreview="true"
            enableWebEdit="true"
            enableDebugger="true"
            disableClientData="false"
            hostName="dev.virginactive"/>
    </sites>
  </sitecore>
</configuration>