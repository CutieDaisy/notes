## Resolving CONTROLLER/ACTION on IIS 
<system.webServer>
  <handlers>
    <remove name="WebDAV" />
  </handlers>
  <security>
    <requestFiltering>
      <verbs allowUnlisted="true">
        <add verb="POST" allowed="true" />
        <add verb="GET" allowed="true" />
      </verbs>
    </requestFiltering>
  </security>
</system.webServer>
