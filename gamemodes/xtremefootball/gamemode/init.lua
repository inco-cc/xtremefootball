AddCSLuaFile("cl_init.lua")

include("shared.lua")

function GM:InitPostEntity()
	if not announcer.IsValid() then
		local announcer = ents.Create("announcer_s4")

		if announcer:IsValid() then
			announcer:Spawn()
		end
	end
end
