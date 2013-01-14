<?xml version="1.0" encoding="utf-8" ?>
<configuration xmlns:patch="http://www.sitecore.net/xmlconfig/" xmlns:set="http://www.sitecore.net/xmlconfig/set/">
  <sitecore>
    <sites>
      <site name="shell" set:content="web" />
      <site name="modules_shell" set:content="web" />
      <site name="testing">
        <patch:delete />
      </site>
    </sites>
    <IDTable>
      <param connectionStringName="master" set:connectionStringName="web" />
    </IDTable>
    <databases>
      <database id="master">
        <patch:delete />
      </database>
    </databases>
    <search>
      <configuration>
        <indexes>
          <index>
            <locations>
              <master>
                <patch:delete />
              </master>
            </locations>
          </index>
        </indexes>
      </configuration>
    </search>
    <scheduling>
      <agent type="Sitecore.Tasks.CleanupFDAObsoleteMediaData">
        <databases hint="raw:AddDatabase">
          <database name="master">
            <patch:delete />
          </database>
        </databases>
      </agent>
      <agent type="Sitecore.Tasks.DatabaseAgent">
         <patch:delete />
      </agent>
      <agent type="Sitecore.Tasks.DatabaseAgent">
         <patch:delete />
      </agent>
      <agent type="Sitecore.Tasks.DatabaseAgent" method="Run" interval="00:10:00">
        <param desc="database">core</param>
        <param desc="schedule root">/sitecore/system/tasks/schedules</param>
        <LogActivity>true</LogActivity>
      </agent>
    </scheduling>

    <settings>
      <!-- extra setting to bypass analytics error -->
      <setting name="Analytics.DefaultDefinitionDatabase" value="web" />
    </settings>
  </sitecore>
</configuration>