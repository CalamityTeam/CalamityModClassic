using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point1.Items;

namespace CalamityModClassic1Point1.Items.Weapons {
public class RaidersGlory : ModItem
{
	public override bool IsLoadingEnabled(Mod mod)/* tModPorter Suggestion: If you return false for the purposes of manual loading, use the [Autoload(false)] attribute on your class instead */
	{
		//texture =("CalamityModClassic1Point1/Items/Weapons/RaidersGlory");
		return true;
	}
	
    public override void SetDefaults()
    {
        //Tooltip.SetDefault("Raider's Glory");
        Item.damage = 37;
        Item.DamageType = DamageClass.Ranged;
        Item.crit += 10;
        Item.width = 58;
        Item.height = 22;
        Item.useTime = 20;
        Item.useAnimation = 20;
        Item.useStyle = 5;
        Item.noMelee = true; //so the item's animation doesn't do damage
        Item.knockBack = 2.25f;
        Item.value = 100000;
        Item.rare = 5;
        Item.UseSound = SoundID.Item5;
        Item.autoReuse = true;
        Item.shoot = 10; //idk why but all the guns in the vanilla source have this
        Item.shootSpeed = 15f;
        Item.useAmmo = 40;
    }
}}