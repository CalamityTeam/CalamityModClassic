using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point1.Items;

namespace CalamityModClassic1Point1.Items.Weapons {
public class CalamitasInferno : ModItem
{
	public override bool IsLoadingEnabled(Mod mod)/* tModPorter Suggestion: If you return false for the purposes of manual loading, use the [Autoload(false)] attribute on your class instead */
	{
		//texture =("CalamityModClassic1Point1/Items/Weapons/6.Calamitas/CalamitasInferno");
		return true;
	}
	
    public override void SetDefaults()
    {
        //Tooltip.SetDefault("Lashes of Chaos");
        Item.damage = 80;
        Item.DamageType = DamageClass.Magic;
        Item.mana = 20;
        Item.width = 28;
        Item.height = 30;
        Item.useTime = 25;
        Item.useAnimation = 25;
        Item.useStyle = 5;
        ////Tooltip.SetDefault("Casts brimstone hellfireballs");
        ////Tooltip.SetDefault("Watch the world burn...");
        Item.noMelee = true; //so the item's animation doesn't do damage
        Item.knockBack = 7.5f;
        Item.value = 550000;
        Item.rare = 7;
        Item.autoReuse = true;
        Item.shoot = Mod.Find<ModProjectile>("BrimstoneHellfireballFriendly").Type;
        Item.shootSpeed = 16f;
    }
}}