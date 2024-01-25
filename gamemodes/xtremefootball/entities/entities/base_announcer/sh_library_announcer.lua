AddCSLuaFile()

module("announcer", package.seeall)

function Set(announcer)
	SetGlobalEntity("Announcer", announcer)
end

function Get()
	return GetGlobalEntity("Announcer", NULL)
end

function IsValid()
	return Get():IsValid()
end

function Say(phrase)
	local announcer = Get()

	if announcer:IsValid() then
		announcer:Say(phrase)
	end
end
