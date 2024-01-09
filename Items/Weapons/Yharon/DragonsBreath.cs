using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point2.Items;

namespace CalamityModClassic1Point2.Items.Weapons.Yharon {
public class DragonsBreath : ModItem
{
	public override void SetStaticDefaults()
	{
		//DisplayName.SetDefault("Dragon's Breath");
		//Tooltip.SetDefault("80% chance to not consume ammo");
	}

    public override void SetDefaults()
    {
        Item.damage = 185;
        Item.DamageType = DamageClass.Ranged;
        Item.width = 64;
        Item.height = 28;
        Item.useTime = 20;
        Item.useAnimation = 20;
        Item.useStyle = ItemUseStyleID.Shoot;
        Item.noMelee = true;
        Item.knockBack = 6.5f;
        Item.value = 10000000;
        Item.UseSound = SoundID.Item36;
        Item.autoReuse = true;
        Item.shoot = ProjectileID.PurificationPowder;
        Item.shootSpeed = 12f;
        Item.useAmmo = 97;
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
	    for (int i = 0; i <= 12; i++)
	    {
	    	float SpeedX = velocity.X + (float) Main.rand.Next(-65, 66) * 0.05f;
	    	float SpeedY = velocity.Y + (float) Main.rand.Next(-65, 66) * 0.05f;
	    	Projectile.NewProjectile(source, position.X, position.Y, SpeedX, SpeedY, Mod.Find<ModProjectile>("DragonBurst").Type, damage, knockback, player.whoAmI, 0.0f, 0.0f);
	    }
	    return false;
	}
    
    public override bool CanConsumeAmmo(Item ammo, Player player)
    {
    	if (Main.rand.Next(0, 100) <= 80)
    		return false;
    	return true;
    }
}}