using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point2.Items;

namespace CalamityModClassic1Point2.Items.Weapons.Yharon {
public class ChickenCannon : ModItem
{
	public override void SetStaticDefaults()
	{
		//DisplayName.SetDefault("Chicken Cannon");
		//Tooltip.SetDefault("Fires chicken rockets");
	}

    public override void SetDefaults()
    {
        Item.damage = 160;
        Item.DamageType = DamageClass.Ranged;
        Item.width = 76;
        Item.height = 24;
        Item.useTime = 13;
        Item.useAnimation = 13;
        Item.useStyle = ItemUseStyleID.Shoot;
        Item.noMelee = true; //so the item's animation doesn't do damage
        Item.knockBack = 8.5f;
        Item.value = 10000000;
        Item.UseSound = SoundID.Item11;
        Item.autoReuse = true;
        Item.shootSpeed = 24f;
        Item.shoot = Mod.Find<ModProjectile>("Chicken").Type;
        Item.useAmmo = 771;
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
    
    public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
    	Projectile.NewProjectile(source, position.X, position.Y, velocity.X, velocity.Y, Mod.Find<ModProjectile>("Chicken").Type, damage, knockback, player.whoAmI, 0.0f, 0.0f);
    	return false;
	}
}}