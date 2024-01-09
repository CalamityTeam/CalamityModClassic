using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point2.Items;

namespace CalamityModClassic1Point2.Items.Weapons.Yharon {
public class ProfanedTrident : ModItem
{
	public override void SetStaticDefaults()
	{
		//DisplayName.SetDefault("Profaned Trident");
	}

	public override void SetDefaults()
	{
		Item.width = 72;  //The width of the .png file in pixels divided by 2.
		Item.damage = 590;  //Keep this reasonable please.
		Item.DamageType = DamageClass.Throwing;  //Dictates whether this is a melee-class weapon.
		Item.noMelee = true;
		Item.noUseGraphic = true;
		Item.useAnimation = 13;
		Item.useStyle = ItemUseStyleID.Swing;
		Item.useTime = 13;
		Item.knockBack = 8f;  //Ranges from 1 to 9.
		Item.UseSound = SoundID.Item1;
		Item.autoReuse = true;  //Dictates whether the weapon can be "auto-fired".
		Item.height = 72;  //The height of the .png file in pixels divided by 2.
		Item.maxStack = 1;
		Item.value = 10000000;  //Value is calculated in copper coins.
		Item.shoot = Mod.Find<ModProjectile>("ProfanedTrident").Type;
		Item.shootSpeed = 28f;
	}
	
	public override void ModifyTooltips(List<TooltipLine> list)
    {
        foreach (TooltipLine line2 in list)
        {
            if (line2.Mod == "Terraria" && line2.Name == "ItemName")
            {
                line2.OverrideColor = new Color(43, 96, 222);
            }
        }
    }
}}
