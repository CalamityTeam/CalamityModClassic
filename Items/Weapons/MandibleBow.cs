using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point1.Items;

namespace CalamityModClassic1Point1.Items.Weapons {
public class MandibleBow : ModItem
{
	public override bool IsLoadingEnabled(Mod mod)/* tModPorter Suggestion: If you return false for the purposes of manual loading, use the [Autoload(false)] attribute on your class instead */
	{
		//texture =("CalamityModClassic1Point1/Items/Weapons/MandibleBow");
		return true;
	}
	
    public override void SetDefaults()
    {
        //Tooltip.SetDefault("Mandible Bow");
        Item.damage = 13;
        Item.DamageType = DamageClass.Ranged;
        Item.width = 22;
        Item.height = 40;
        Item.useTime = 25;
        Item.useAnimation = 25;
        Item.useStyle = 5;
        Item.noMelee = true; //so the item's animation doesn't do damage
        Item.knockBack = 2f;
        Item.value = 15000;
        Item.rare = 1;
        Item.UseSound = SoundID.Item5;
        Item.autoReuse = false;
        Item.shoot = 10; //idk why but all the guns in the vanilla source have this
        Item.shootSpeed = 30f;
        Item.useAmmo = 40;
    }
}}