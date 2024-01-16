using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point1.Items;

namespace CalamityModClassic1Point1.Items.Weapons {
public class PlasmaRod : ModItem
{
	public override bool IsLoadingEnabled(Mod mod)/* tModPorter Suggestion: If you return false for the purposes of manual loading, use the [Autoload(false)] attribute on your class instead */
	{
		//texture =("CalamityModClassic1Point1/Items/Weapons/PlasmaRod");
		return true;
	}
	
    public override void SetDefaults()
    {
        //Tooltip.SetDefault("Plasma Rod");
        Item.damage = 10;
        Item.DamageType = DamageClass.Magic;
        Item.mana = 10;
        Item.width = 40;
        Item.height = 40;
        Item.useTime = 36;
        Item.useAnimation = 36;
        Item.useStyle = 5;
        Item.staff[Item.type] = true;
        ////Tooltip.SetDefault("Casts a low-damage plasma bolt\nShooting a tile will cause several bolts with increased damage to fire\nShooting an enemy will cause several debuffs for a short time");
        Item.noMelee = true; //so the item's animation doesn't do damage
        Item.knockBack = 2.5f;
        Item.value = 60000;
        Item.rare = 3;
        Item.UseSound = SoundID.Item109;
        Item.autoReuse = true;
        Item.shoot = Mod.Find<ModProjectile>("PlasmaRay").Type;
        Item.shootSpeed = 6f;
    }
}}