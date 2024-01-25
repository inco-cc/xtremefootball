AddCSLuaFile()

local registry = debug.getregistry()

if not istable(registry.Announcer) then
	registry.Announcer = {}
end

local Entity = registry.Entity
local Announcer = registry.Announcer

Announcer.MetaName = "Announcer"
Announcer.MetaID = Entity.MetaID
Announcer.MetaBaseClass = Entity

function Announcer:__index(key)
	if Announcer[key] ~= nil then
		return Announcer[key]
	else
		return Entity.__index(self, key)
	end
end

function Announcer:__tostring()
	return string.format("Announcer [%i][%s]",
	    self:EntIndex(), self:GetClass())
end

function Announcer:IsAnnouncer()
	return true
end
