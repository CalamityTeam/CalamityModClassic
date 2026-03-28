using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassicPreTrailer.Items.Armor;

namespace CalamityModClassicPreTrailer.Items.Armor
{
    [AutoloadEquip(EquipType.Head)]
    public class DaedalusHeadgear : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Daedalus Circlet");
            /* Tooltip.SetDefault("5% increased minion damage and +2 max minions\n" +
                "Immune to Cursed and gives control over gravity"); */
        }

        public override void SetDefaults()
        {
            Item.width = 18;
            Item.height = 18;
			Item.value = Item.buyPrice(0, 25, 0, 0);
			Item.rare = 5;
            Item.defense = 3; //33
        }

        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return body.type == Mod.Find<ModItem>("DaedalusBreastplate").Type && legs.type == Mod.Find<ModItem>("DaedalusLeggings").Type;
        }

        public override void ArmorSetShadows(Player player)
        {
            player.armorEffectDrawShadowSubtle = true;
            player.armorEffectDrawOutlines = true;
        }

        public override void UpdateArmorSet(Player player)
        {
            player.setBonus = "20% increased minion damage\n" +
                "A daedalus crystal floats above you to protect you";
            CalamityPlayerPreTrailer modPlayer = player.GetModPlayer<CalamityPlayerPreTrailer>();
            modPlayer.daedalusCrystal = true;
            if (player.whoAmI == Main.myPlayer)
            {
                if (player.FindBuffIndex(Mod.Find<ModBuff>("DaedalusCrystal").Type) == -1)
                {
                    player.AddBuff(Mod.Find<ModBuff>("DaedalusCrystal").Type, 3600, true);
                }
                if (player.ownedProjectileCounts[Mod.Find<ModProjectile>("DaedalusCrystal").Type] < 1)
                {
                    Projectile.NewProjectile(Entity.GetSource_FromThis(null),player.Center.X, player.Center.Y, 0f, -1f, Mod.Find<ModProjectile>("DaedalusCrystal").Type, (int)(95f * player.GetDamage(DamageClass.Summon).Multiplicative), 0f, Main.myPlayer, 0f, 0f);
                }
            }
            player.GetDamage(DamageClass.Summon) += 0.2f;
        }

        public override void UpdateEquip(Player player)
        {
            player.GetDamage(DamageClass.Summon) += 0.05f;
            player.maxMinions += 2;
            player.AddBuff(BuffID.Gravitation, 2);
            player.buffImmune[BuffID.Cursed] = true;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(null, "VerstaltiteBar", 8);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();
        }
    }
}