using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point2.Items;

namespace CalamityModClassic1Point2.Items.Weapons.Providence {
public class MoltenAmputator : ModItem
{
	public override void SetStaticDefaults()
	{
		//DisplayName.SetDefault("Molten Amputator");
	}

	public override void SetDefaults()
	{
		Item.width = 60;  //The width of the .png file in pixels divided by 2.
		Item.damage = 280;  //Keep this reasonable please.
		Item.noMelee = true;  //Dictates whether this is a melee-class weapon.
		Item.noUseGraphic = true;
		Item.autoReuse = true;
		Item.useAnimation = 18;
		Item.useStyle = ItemUseStyleID.Swing;
		Item.useTime = 18;
		Item.knockBack = 9f;  //Ranges from 1 to 9.
		Item.UseSound = SoundID.Item1;
		Item.DamageType = DamageClass.Throwing;  //Dictates whether the weapon can be "auto-fired".
		Item.height = 60;  //The height of the .png file in pixels divided by 2.
		Item.value = 5000000;  //Value is calculated in copper coins.
		Item.shoot = Mod.Find<ModProjectile>("MoltenAmputator").Type;
		Item.shootSpeed = 12f;
	}
	
	public override void ModifyTooltips(List<TooltipLine> list)
    {
        foreach (TooltipLine line2 in list)
        {
            if (line2.Mod == "Terraria" && line2.Name == "ItemName")
            {
                line2.OverrideColor = new Color(0, 255, 200);
            }
        }
    }
}}
